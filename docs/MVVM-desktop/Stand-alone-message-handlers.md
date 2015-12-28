We have analyzed why we need a [[messaging system|The message broker]]  and how to interact with it at runtime sending and receiving messages.

There are scenarios in which the code that receive the message has nothing to do with the UI so there is no reason to subscribe to that message in a `ViewModel`, in this cases we can use stand alone message handlers to have a class that will be instantiated and executed at runtime each time a new message, in which we are interested, is received:

```csharp
class MyMessageHandler : AbstractMessageHandler<MyMessage>
{
    public override void Handle( object sender, MyMessage message )    {
        //handle my message here.
    }}
```

Given the default Radical [[Bootstrap conventions]] if the above class is defined in a namespace ending with `*.Messaging.Handlers` it will be automatically registered into the chosen container and invoked whenever a message of type `MyMessage` si delivered by the `MessageBroker`.