## MementoObserver

A memento observer, or monitor, is an instance of the `MementoObserver` class that is capable to observe, and react, to changes that occurs to a `ChangeTrackingService` instance.

```csharp
var memento = new ChangeTrackingService();
var monitor = MementoObserver.Monitor( memento );
```

We can use a monitor to trigger, for example, the `CanExecuteChanged` event of a `ICommand` interface implementation:

```csharp
var memento = new ChangeTrackingService();
var monitor = MementoObserver.Monitor( memento );

DelegateCommand.Create()
    .OnCanExecute( state =>
    {
        //evaluate if the command can be executed.
        return true;
    } )
    .OnExecute( state =>
    {
        //execute the command
    } )
    .AddMonitor( monitor );
```

The above code is not very different from manually attaching the `TrackingStateChanged` event of the `ChangeTrackingService` instance and manually calling the `RaiseCanExecuteChanged` method of the `DelegateCommand` instance, it is simply more concise and easier to maintain.