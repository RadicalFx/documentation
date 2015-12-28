WPF and Universal Applications have a really handy way to handle the concept of a command: the `ICommand` interface (http://msdn.microsoft.com/library/system.windows.input.icommand.aspx).

Radical has its own implementation that allows to easily hook command logic using delegates and adds a set of features on top of the default .Net `ICommand`.

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