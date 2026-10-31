# The async-only message broker in WPF and Avalonia applications

> **Proposal.** This guide describes the changes proposed in [RadicalFx/Radical#749](https://github.com/RadicalFx/Radical/issues/749). The API below isn't final, and no released version implements it yet.

Radical.Windows and Radical.Avalonia applications need the changes in [upgrade to the async-only message broker](async-message-broker.md), plus the ones below. Once view models await the broker, their commands, the shutdown process and the built-in messages need to work with tasks too.

## Commands

### Auto command binding

Today [`AutoCommandBinding`](../markup/auto-command-binding.md) invokes view model methods through a `void` delegate. An `async Task` method compiles, but its task is discarded:
- exceptions are lost
- nothing knows the command is still running
- clicking twice runs it twice

The proposal is that `AutoCommandBinding` supports methods returning a `Task`:
- the command awaits the method
- while it runs, the command can't execute again, so bound controls are disabled
- exceptions go to the application's unhandled exception handler, configured through `OnUnhandledException`

```csharp
// before
class CustomerViewModel : AbstractViewModel
{
    public void Save()
    {
        broker.Dispatch(this, new CustomerSaving(customer));
        repository.Save(customer);
    }

    public bool CanSave => IsValid;
}

// after
class CustomerViewModel : AbstractViewModel
{
    public async Task Save()
    {
        await broker.DispatchAsync(this, new CustomerSaving(customer));
        await repository.SaveAsync(customer);
    }

    public bool CanSave => IsValid;
}
```

The XAML doesn't change: `{markup:AutoCommandBinding Path=Save}` binds the async method like the synchronous one. The `Can*` property convention and its monitoring don't change either.

### DelegateCommand

[`DelegateCommand`](../mvvm/delegate-command.md) gets an `OnExecute` overload taking an async delegate, with the same running and exception behavior:

```csharp
// before
ICommand command = DelegateCommand.Create()
    .OnExecute(state => broker.Dispatch(this, new CustomerSaving(customer)));

// after
ICommand command = DelegateCommand.Create()
    .OnExecute(async state => await broker.DispatchAsync(this, new CustomerSaving(customer)));
```

The synchronous `OnExecute(Action<object>)` overload stays and isn't deprecated: commands that don't await anything, such as toggling a property, keep using it. The same applies to `AutoCommandBinding`, which keeps binding `void` methods.

The compiler picks the async overload for any lambda returning a `Task`, such as `state => broker.DispatchAsync(...)`. A block lambda that calls an async method without awaiting or returning its task, such as `state => { broker.DispatchAsync(...); }`, still binds to the synchronous overload and the task is discarded. Use `async` and `await` in block lambdas.

## Application shutdown

`ApplicationShutdownRequested` is the message whose `Cancel` property lets subscribers stop a [user-requested shutdown](../mvvm/application-shutdown.md). The application now awaits its subscribers before reading `Cancel`. Subscribers can therefore await, for example, an asynchronous "save changes?" dialog:

```csharp
// before
broker.Subscribe<ApplicationShutdownRequested>(this, InvocationModel.Safe, (sender, message) =>
{
    message.Cancel = HasChanges && !ConfirmDiscardChanges();
});

// after
broker.Subscribe<ApplicationShutdownRequested>(this, InvocationModel.Safe, async (sender, message) =>
{
    message.Cancel = HasChanges && !await ConfirmDiscardChangesAsync();
});
```

Starting the shutdown changes as follows:

| Today | Async-only |
|---|---|
| `RadicalApplication.Shutdown()` | `await RadicalApplication.ShutdownAsync()` |
| Broadcasting `ApplicationShutdownRequest` | `await broker.BroadcastAsync(this, new ApplicationShutdownRequest())`, or fire-and-forget |
| `Application.Current.Shutdown()` (WPF), or the lifetime's `Shutdown()` (Avalonia) | Unchanged. It still can't be canceled, and still raises `ApplicationShutdown` |

The platform's own end-of-session notifications (`SessionEnding` in WPF) are synchronous and don't go through `ApplicationShutdownRequested`, so they're unaffected.

## Built-in messages

[Built-in messages](../mvvm/built-in-messages.md) such as `ViewLoaded`, `ViewModelLoaded`, `ViewModelShown` and `ViewModelClosed` are broadcast by the infrastructure from synchronous UI events like `Loaded`, `Activated` and `Closed`. They stay fire-and-forget, as today. Their subscribers follow the [subscriber changes](async-message-broker.md#subscribers).

Messages the application sends to the infrastructure, such as `CloseViewRequest`, become `BroadcastAsync`:

```csharp
// before
broker.Broadcast(this, new CloseViewRequest(this));

// after
await broker.BroadcastAsync(this, new CloseViewRequest(this));
```

`ViewModelClosing` is documented as dispatched synchronously so that subscribers can cancel a close. Neither library publishes it today: closing a view is canceled through the `IExpectViewClosingCallback` [callback expectation](../mvvm/callback-expectations.md), which doesn't use the broker and doesn't change.

## UI thread and invocation model

`InvocationModel.Safe` doesn't change: the broker starts the callback on the UI thread. After an `await` inside the callback, execution resumes on the UI thread, captured by the UI synchronization context, so updating bound properties after awaiting is safe:

```csharp
broker.Subscribe<CustomerSelected>(this, InvocationModel.Safe, async (sender, message) =>
{
    var customer = await repository.LoadAsync(message.CustomerId);
    Customer = customer; //back on the UI thread
});
```

Avoid `ConfigureAwait(false)` in callbacks that touch the UI afterwards.

## What doesn't change

- **Monitors:** `PropertyObserver`, `MementoObserver` and the `Can*` property monitoring that re-evaluates a command's `CanExecute`. They're synchronous UI notifications.
- **View lifecycle callback expectations** (`IExpectViewLoadedCallback`, `IExpectViewClosingCallback` and the others).
- **Regions, the view resolver, and the bootstrap conventions.** Standalone handlers in `*.Messaging.Handlers` namespaces are still registered automatically, implementing `HandleAsync`.

## Upgrade checklist

- [ ] [The message broker changes](async-message-broker.md#upgrade-checklist) are done.
- [ ] View model command methods that await become `async Task`, and none is `async void`.
- [ ] `DelegateCommand.OnExecute` delegates that await use the async overload.
- [ ] `ApplicationShutdownRequested` subscribers await before setting `Cancel`.
- [ ] `RadicalApplication.Shutdown()` becomes `await RadicalApplication.ShutdownAsync()`.
- [ ] `Broadcast` calls to the infrastructure, such as `CloseViewRequest`, become `BroadcastAsync`.
- [ ] No `.Wait()` or `.Result` on broker tasks, especially on the UI thread.
