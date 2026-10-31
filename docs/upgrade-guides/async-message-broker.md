# Upgrade to the async-only message broker

> **Proposal.** This guide describes the changes proposed in [RadicalFx/Radical#749](https://github.com/RadicalFx/Radical/issues/749). The API below isn't final, and no released version implements it yet.

The message broker becomes async-only: subscribers, standalone handlers and publishers all use `Task`-based APIs. This guide applies to every application using the Radical message broker. WPF and Avalonia applications also need the changes in [the async-only message broker in WPF and Avalonia applications](async-message-broker-ui.md).

## Proposed API

| Today | Async-only |
|---|---|
| `Subscribe<T>(subscriber, Action<object, T> callback)` | `Subscribe<T>(subscriber, Func<object, T, Task> callback)` |
| `Subscribe<T>(subscriber, Func<object, T, bool> callbackFilter, Action<object, T> callback)` | `Subscribe<T>(subscriber, Func<object, T, bool> callbackFilter, Func<object, T, Task> callback)` |
| `Dispatch(sender, message)` | `Task DispatchAsync(sender, message)` |
| `Broadcast(sender, message)` | `Task BroadcastAsync(sender, message)`, which already exists |
| `IHandleMessage<T>.Handle(object sender, T message)` | `Task HandleAsync(object sender, T message)` |
| `AbstractMessageHandler<T>.Handle(object sender, T message)` | `Task HandleAsync(object sender, T message)` |

Everything else stays as it is:
- the overloads with a sender filter, an `InvocationModel` or a message type
- `callbackFilter`, which remains synchronous: filters should be cheap and side-effect free
- `Unsubscribe`
- the [inheritance support](../messaging/message-broker.md#inheritance-support)

## Subscribers

Subscription callbacks return a `Task`. Callbacks that do async work become `async` lambdas:

```csharp
// before
broker.Subscribe<CustomerSelected>(this, (sender, message) =>
{
    Customer = repository.Load(message.CustomerId);
});

// after
broker.Subscribe<CustomerSelected>(this, async (sender, message) =>
{
    Customer = await repository.LoadAsync(message.CustomerId);
});
```

Callbacks with nothing to await return a completed task:

```csharp
broker.Subscribe<CustomerSelected>(this, (sender, message) =>
{
    SelectedCustomerId = message.CustomerId;
    return Task.CompletedTask;
});
```

Don't keep a callback synchronous by making it `async void`: the broker can't observe its exceptions or wait for it to complete.

## Standalone message handlers

[Standalone message handlers](../messaging/abstract-message-handler.md) implement `HandleAsync` instead of `Handle`:

```csharp
// before
class MyMessageHandler : AbstractMessageHandler<MyMessage>
{
    public override void Handle(object sender, MyMessage message)
    {
        //handle my message here.
    }
}

// after
class MyMessageHandler : AbstractMessageHandler<MyMessage>
{
    public override async Task HandleAsync(object sender, MyMessage message)
    {
        //handle my message here, awaiting what needs to be awaited.
    }
}
```

Handlers implementing `IHandleMessage<T>` directly change in the same way. `ShouldHandle` remains synchronous.

## Publishers

### Dispatch

`Dispatch` invoked the subscribers on the publisher's thread, one at a time, and returned once all of them completed. `DispatchAsync` keeps those semantics, awaiting each subscriber in turn, so the publisher awaits it:

```csharp
// before
public void Save()
{
    broker.Dispatch(this, new CustomerSaving(customer));
    repository.Save(customer);
}

// after
public async Task SaveAsync()
{
    await broker.DispatchAsync(this, new CustomerSaving(customer));
    await repository.SaveAsync(customer);
}
```

The method calling `DispatchAsync` becomes `async`, and so do its callers. In a UI application this usually stops at a command handler; see [commands](async-message-broker-ui.md#commands).

Code that reads the message after dispatching it, for example a `Cancel` flag set by the subscribers, keeps working, as long as it awaits `DispatchAsync` before reading.

### Broadcast

`Broadcast` was already asynchronous: it invoked the subscribers in parallel, on thread pool threads, without waiting for them. `BroadcastAsync` does the same, and its task completes once all the subscribers completed:

```csharp
// before
broker.Broadcast(this, new CustomerSaved(customer));

// after: wait for the subscribers, observing their exceptions
await broker.BroadcastAsync(this, new CustomerSaved(customer));

// after: fire-and-forget, as Broadcast did
_ = broker.BroadcastAsync(this, new CustomerSaved(customer));
```

With fire-and-forget, nothing observes the subscribers' exceptions: subscribers must handle their own errors.

## Never block on the broker

Don't wait for the broker's tasks synchronously:

```csharp
// don't
broker.DispatchAsync(this, message).Wait();
var result = broker.DispatchAsync(this, message).Result;
broker.DispatchAsync(this, message).GetAwaiter().GetResult();
```

On a UI thread, with subscribers marshalled to the UI thread (`InvocationModel.Safe`), this deadlocks: the UI thread waits for a subscriber that needs the UI thread to run. That's the sync-over-async design flaw the async-only broker removes. Always `await`, all the way up to the command or event handler that started the operation.

## Upgrade checklist

- [ ] Every `Subscribe` callback returns a `Task`, and none is `async void`.
- [ ] Every `AbstractMessageHandler<T>` and `IHandleMessage<T>` implements `HandleAsync`.
- [ ] Every `Dispatch` becomes `await DispatchAsync`, and its callers are `async`.
- [ ] Every `Broadcast` becomes `await BroadcastAsync`, or a deliberate fire-and-forget `_ = BroadcastAsync`.
- [ ] No `.Wait()`, `.Result` or `.GetAwaiter().GetResult()` on broker tasks.
- [ ] WPF and Avalonia applications: [the UI-specific changes](async-message-broker-ui.md).
