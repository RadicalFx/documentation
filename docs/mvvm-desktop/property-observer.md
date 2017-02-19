The simplest observer, or monitor, in the Radical framework is a `PropertyObserver` the role of a property observer is to monitor property changes of a class implementing the `INotifyPropertyChanged` interface. We can monitor single properties:

```csharp
var monitor = PropertyObserver.For( person )
```

Or we can monitor the entire entity being notified each time a property changes:

```csharp
var monitor = PropertyObserver.ForAllPropertiesOf( person );
```

We can use a monitor to trigger, for example, the `CanExecuteChanged` event of a `ICommand` interface implementation:

```csharp
var monitor = PropertyObserver.ForAllPropertiesOf( person );
DelegateCommand.Create()
        //evaluate if the command can be executed.
    .OnExecute( state =>
    {
        //execute the command
    } )
    .AddMonitor( monitor );
```

In the above sample each time one of the property of the `Person` instance changes the command state will be evaluated for execution allowing the command to change its `CanExecute` state without polling anything but simply waiting to be notified.