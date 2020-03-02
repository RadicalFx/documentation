## MementoObserver

A memento observer, or monitor, is an instance of the `MementoObserver` class that observes changes occurring to a `ChangeTrackingService` instance.

```csharp
var memento = new ChangeTrackingService();
var monitor = MementoObserver.Monitor(memento);
```

We can use a monitor to trigger, for example, the `CanExecuteChanged` event of a `ICommand` interface implementation:

```csharp
var memento = new ChangeTrackingService();
var monitor = MementoObserver.Monitor(memento);

var saveCommand = DelegateCommand.Create()
    .OnCanExecute(state => memento.IsChanged)
    .OnExecute(state => /* execute the command */)
    .AddMonitor(monitor);
```

The above code is not very different from manually attaching the `TrackingStateChanged` event of the `ChangeTrackingService` instance and manually calling the `RaiseCanExecuteChanged` method of the `DelegateCommand` instance, it is simply more concise and easier to maintain.
