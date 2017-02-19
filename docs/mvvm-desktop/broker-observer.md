As for the [[PropertyObserver]] or the [[MementoObserver]] the BrokerObserver is an easy shortcut to react to message arrivals when using [[the message broker]].

```csharp
var broker = //instance of a IMessageBroker implementation.
var monitor = BrokerObserver.Using( broker )
    .WaitingFor<MySampleMessage>();
```

We can use a monitor to trigger, for example, the `CanExecuteChanged` event of a `ICommand` interface implementation:

```csharp
var broker = //instance of a IMessageBroker implementation.
var monitor = BrokerObserver.Using( broker )
    .WaitingFor<MySampleMessage>();

DelegateCommand.Create()
        //evaluate if the command can be executed.
    .OnExecute( state =>
    {
        //execute the command
    } )
    .AddMonitor( monitor );
```

What the above code does is to trigger the `CanExecuteChanged` logic each time a message of type `MySampleMessage` is delivered via the monitored message broker.