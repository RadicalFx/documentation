## Commands and DelegateCommand

WPF and Universal Applications have a really handy way to handle the concept of a command: the `ICommand` interface (http://msdn.microsoft.com/library/system.windows.input.icommand.aspx).

Radical has its own implementation that allows to easily hook command logic using delegates. The Radical `DelegateCommand` adds a set of features on top of the default .Net `ICommand`.

Creating a command in Radical is as easy as:

```csharp
ICommand command =  DelegateCommand.Create()
    .OnCanExecute( state =>
    {
        //command validation logic.
        return true;
     } )
     .OnExecute( state =>
     {
         //command execution logic.
     } );
```

The command entry point is the `DelegateCommand` class, in the above sample used in a fluent interface manner.

### Asynchronous execution

The execution logic can also be asynchronous:

```csharp
ICommand command = DelegateCommand.Create()
    .OnExecute( async state =>
    {
        await broker.DispatchAsync( this, new CustomerSaving( customer ) );
        await repository.SaveAsync( customer );
    } );
```

The command awaits the delegate:
- while it runs, the command can't execute again, so bound controls are disabled;
- exceptions go to the application's unhandled exception handler.

Code that needs to wait for the command, such as a test, awaits `ExecuteAsync` instead of calling `Execute`. A command has either synchronous or asynchronous execution logic: setting one replaces the other. Use `async` and `await` in block lambdas: `state => { broker.DispatchAsync(...); }` binds to the synchronous overload, and the task is discarded.