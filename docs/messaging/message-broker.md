## Message Broker

The message broker pattern is a way to decouple the sender of a message and the subscribers of that message. In a standard event-based approach the subscriber needs in order to subscribe to an event:

  1. a reference to the publisher;
  2. knowledge of the event signature;

In lots of cases we need to be able to let 2 different components talk to each other in a more decoupled way since we have no easy way to satisfy the first point, in this cases introducing a third actor, the broker, that both knows is a really simple way to solve the original problem:

![Messaging diagram](../images/message-broker-1.png)

Radical has its own built-in broker implementation represented by the `IMessageBroker` interface and by the default `MessageBroker` implementation found in the Radical assembly.

### Usage

The first thing we need to do is to create an instance of the broker:

```csharp
var broker = new MessageBroker(new NullDispatcher());
```

> The broker itself has a dependency on the `IDispatcher` interface, an `IDispatcher` can be seen as a wrapper of the current `SynchronizationContext`. We wrap it in a `IDispatcher` instance to avoid coupling the broker to a specific implementation.

> In the above sample we are using a default `NullDispatcher` that does nothing and is a good choice for console or web applications where marshaling calls in the main thread is not mandatory. Radical.Windows comes with a built-in `WpfDispatcher`.

Once we have created the broker we can share it among all the components that need it:

```csharp
var sampleSender = new SenderComponent(broker);
var sampleReceiver = new ReceiverComponent(broker);
```

The third thing we need is something to exchange between components:

```csharp
class SampleMessage
{
}
```

> [POCO messages](poco-messages.md) are fully supported.

NOTE: It's also important to notice that all messages are shared in memory between publishers and subscribers, which means that there won't be any serialization happening and messages don't need to be serializable.

Now that we have 2 components, a broker and something that we want to share from one component to the other we can use it in the following manner:

```csharp
class SenderComponent
{
    IMessageBroker broker;

    public SenderComponent(IMessageBroker broker)
    {
        this.broker = broker;
    }

    public Task PublishAsync()
    {
        return this.broker.BroadcastAsync(sender: this, message: new SampleMessage());
    }
}
```

and from the receiver point of view:

```csharp
class ReceiverComponent
{    
    IMessageBroker broker;

    public SenderComponent( IMessageBroker broker )
    {
        this.broker = broker;
        this.broker.Subscribe<SampleMessage>(subscriber: this, (sender, message) =>
        {
            //handle the message here.
          ### Dispatching or broadcasting

The broker has two ways to publish a message, and both return a `Task`:
- `DispatchAsync` invokes the subscribers one at a time, awaiting each before invoking the next, on the publisher's context.
- `BroadcastAsync` invokes the subscribers in parallel, on thread pool threads. Its task completes once all of them completed.

When the subscribers are independent of each other, don't touch the UI and never fail, the two behave the same way. Otherwise they differ in four ways.

**The thread the subscribers run on.** `DispatchAsync` invokes every subscriber on the publisher's context, ignoring the [invocation model](#invocation-model). Dispatched from the UI thread, a subscriber can update bound properties directly. `BroadcastAsync` starts every subscriber on a thread pool thread: a subscriber that accesses the UI must subscribe with `InvocationModel.Safe`.

**Whether subscribers see each other's changes.** Dispatched subscribers run in sequence, so each one sees what the previous ones did to the message. That makes it possible for the publisher to ask its subscribers something, and read the answer once they all completed:

```csharp
var message = new CustomerDeleting(customer);
await broker.DispatchAsync(this, message);
if (!message.Cancel)
{
    await repository.DeleteAsync(customer);
}
```

Broadcast subscribers run concurrently, so a message they modify is shared across threads without synchronization: broadcast messages should be treated as read-only.

**Failures.** When a dispatched subscriber throws, the remaining subscribers aren't invoked, and the exception reaches the publisher right away. When a broadcast subscriber throws, all the other subscribers still run; awaiting `BroadcastAsync` rethrows the first exception, and the task's `Exception` property contains all of them.

**Time and the publisher's thread.** Dispatching takes as long as all the subscribers together, and any synchronous work a subscriber does before its first `await` runs on the publisher's thread, the UI thread in a UI application. Broadcasting takes as long as the slowest subscriber, and leaves the publisher's thread free.

In short, dispatching is asking: the publisher's collaborators run in sequence, in its context, and can influence the outcome. Broadcasting is notifying: the order doesn't matter, and the subscribers must not depend on each other.

A broadcast can be awaited, to wait for the subscribers and observe their exceptions, or deliberately not awaited, with `_ = broker.BroadcastAsync(...)`. When nothing awaits it, nothing observes the subscribers' exceptions, and the subscribers must handle their own errors. Never block on the broker's tasks with `.Wait()`, `.Result` or `.GetAwaiter().GetResult()`: on a UI thread, with subscribers marshalled to the UI thread, it deadlocks.

 the same thread of the publisher in a serial manner.

#### Invocation Model

In our experience the most frequent usage of the broker is within the management of the UI of an application based on the MVVM pattern, which means that in most cases the subscriber of the event needs to access the UI, thus needs to run on the UI/main thread.

If we want to reduce the friction and we do not need to have control on the marshaling process we can ask the broker to automatically call the subscriber on the main thread for us:

```csharp
broker.Subscribe<SampleMessage>(this, InvocationModel.Safe, (sender, message) =>
{
    //this delegate is automatically invoked on the main thread.
    return Task.CompletedTask;
});
```

Using the subscribe overload that accept an `InvocationModel` enumeration we can specify that we, as subscribers, need the subscription to be invoked on the main UI thread.

NOTE: The broadcast operation is still asynchronous and the broker only dispatches on the main thread the given delegate only when required. Only the synchronous part of the delegate is marshalled: once it awaits, it resumes on the main thread through its synchronization context. Dispatched messages ignore the invocation model, since their subscribers already run on the publisher's context.

### Inheritance support

One interesting thing we can do is subscribe to a base class in order to receive all the messages that inherits from the specified type:

```csharp
broker.Subscribe<IMessage>(this, (sender, msg) =>
{
    //all the messages that inherits from IMessage we'll be handled here.
});
```

In the above sample we are basically building a sort of catch all handler.
