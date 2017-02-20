## Message Broker

The message broker pattern is basically a way to decouple the sender of an event/message and the subscribers of that message, in a standard event-based approach the subscriber needs in order to subscribe to an event:

  1. a reference to the publisher;
  2. knowledge of the event “shape”;

In lots of cases we need to be able to let 2 different components speak to each other in a more decoupled way since we have no easy way to satisfy the first point, in this cases introducing a third actor, the broker, that both knows is a really simple way to solve the original problem:

![Messaging diagram](/images/message-broker-1.png)

Radical has its own built-in broker implementation represented by the `IMessageBroker` interface and by the default MessageBroker implementation found in the Radical assembly.

**Usage**

The first thing we need to do is to create an instance of the broker:

```csharp
var broker = new MessageBroker( new NullDispatcher() );
```

>the broker itself has a dependency on the `IDispatcher` interface, an `IDispatcher` is basically a wrapper of the `SynchronizationContext`. We wrap it in a `IDispatcher` instance in order to simplify the sharing of the codebase of the broker among different technologies, such as WPF, WinRT or Silverlight.

>In the above sample we are using a default `NullDispatcher` that does nothing and is ideal in Console or web application where marshaling calls in the main thread is not mandatory. Each Radical specific implementation has its own dispatcher: `WpfDispatcher`, `SilverlightDispatcher`, etc..

Once we have created the broker we can share it among all the components that need it:

```csharp
var sampleSender = new SenderComponent(  broker );
var sampleReceiver = new ReceiverComponent( broker );
```

The third thing we need is something to exchange between components:

```csharp
class SampleMessage : IMessage
{
    public SampleMessage( Object sender )
    {
        this.Sender = sender;
    }

    public Object Sender{ get; private set; }
}
```

>[POCO messages](poco-messages.md) are now fully supported.

Now that we have 2 components, a broker and something that we want to share from one component to the other we can use it in the following manner:

```csharp
class SenderComponent
{
    IMessageBroker broker;

    public SenderComponent( IMessageBroker broker )
    {
        this.broker = broker;
    }

    public void Publish()
    {
        this.broker.Broadcast( new SampleMessage( this ) );

        //the POCO API will be:
        //this.broker.Broadcast( this, new SampleMessage() );
        //without the need for the message to implement the IMessage interface.
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
        this.broker.Subscribe<SampleMessage>( this, msg => 
        {
            //handle the message here.
        } );
    }
}
```

**Dispatch vs. Broadcast**

In the sample above the “sender” utilizes the Broadcast method, broadcasted messages will be delivered to subscribers asynchronously, and in parallel, thus the subscriber is invoked on a thread that is not the same as the publisher.

If we, for some reason, need to be have events dispatched in a synchronous manner we can use the Dispatch method that guarantees that all the subscribers a re invoked on the same thread of the publisher in a serial way.

**InvocationModel**

In our experience the most frequent usage of the broker is within the management of the UI of an application based on the MVVM pattern, in this case in most cases the subscriber of the event needs to access the UI, thus needs to run on the UI/Main thread.

If we want to reduce the friction and we do not need to have control on the marshaling process we can ask the broker to automatically call the subscriber on the main thread for us:

```csharp
this.broker.Subscribe<SampleMessage>( this, InvocationModel.Safe, msg =>
{
    //this delegate is automatically invoked on the main thread.
} );
```

Using the subscribe overload that accept an InvocationModel enum parameter we can specify that we, as subscribers, need that the given delegate must be invoked in the main thread.

Please note that the broadcast is still asynchronous and the broker only dispatches on the main thread the given delegate only when required.

**Subscriptions using inheritance**

One interesting thing we can do is subscribe to a base class in order to receive all the messages that inherits from the specified type:

```csharp
this.broker.Subscribe<IMessage>( this, msg =>
{
    //all the messages that inherits from IMessage we'll be handled also here.
} );
```

In the above sample we are basically building a catch all handler.