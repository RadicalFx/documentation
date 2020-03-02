## Standalone message handlers

We have analyzed why we need a [messaging system](/messaging/message-broker.md)  and how to interact with it at runtime sending and receiving messages.

There are scenarios in which the code that receive the message has nothing to do with the UI so there is no reason to subscribe to that message in a `ViewModel`, in this cases we can use stand alone message handlers to have a class that will be instantiated and executed at runtime each time a new message, in which we are interested, is received:

```csharp
class MyMessageHandler : AbstractMessageHandler<MyMessage>
{
    public override void Handle(object sender, MyMessage message)
    {
        //handle my message here.
    }
}
```

When using [message broker and messages](/messaging/message-broker.md) in the context of a MVVM based application Radical [bootstrap conventions](/mvvm/bootstrap-conventions.md), at boot time, will take care of registering in the IoC container all standalone message handlers that match the convention. That, in this case, is when classes are defined in a namespace ending with `*.Messaging.Handlers`.
