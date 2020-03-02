# Auto Command binding

WPF `ICommand` interface is the canonical way to expose commands from a ViewModel that is bound to a View. They come with a few caveats:

* Most of the times commands are simple and the plumbing required to create an `ICommand` implementation is not worth it.
* ViewModels exposing commands that implement the `ICommand` interface reference WPF types with the risk of complicating the required testing infrastructure.

Radical solves the above issues by introducing a handy markup extension that allow to write a ViewModel like the following:

```csharp
class MyViewModel : AbstractViewModel
{
   public void DoSomething()
   {
      //perform work
   }
}
```

The `DoSomething` method can be bound to a `Button` on the View in the following manner:

```xml
<Button Command="{markup:AutoCommandBinding Path=DoSomething}" />
```

The `AutoCommandBinding` markup extension will dynamically build a [DelegateCommand](/mvvm/delegate-command.md) that wraps at runtime the method invocation.

The `ICommand` interface exposes a `CanExecute(object)` method that the WPF inteface can call to detect if the command is available in the current context and thus decide if the WPF element bound to a command should be enabled or not. Using the same approach as above a ViewModel can expose a `bool` property as following:

```csharp
class MyViewModel : AbstractViewModel
{
   public void DoSomething()
   {
      //perform work
   }
   
   public bool CanDoSomething
   {
      get{ return true; /* or false */ }
   }
}
```

The convention is to expose a public boolean property whose name is the same as the method, that will be wraped in a command, prefixed with `Can`. No changes to the XAML markup are required.

Given the way commands work in WPF one thing that might be required is to change the command status, and thus the bound control, from the ViewModel implementation. The easiest thing is to ask WPF to reevluate the `Can*` boolean property whenever we decide the command status changes. We can leverage the power of [Radical properties](/entities/property-system.md) metadata, and specifically the cascade changes notification feature as following:

```csharp
class MyViewModel : AbstractViewModel
{
   public MyViewModel()
   {
        this.GetPropertyMetadata( () => this.SelectedEntity )
            .AddCascadeChangeNotifications( () => this.CanEdit );
   }
   
   public MyEntity SelectedEntity
   {
      get { return this.GetPropertyValue( () => this.SelectedEntity ); }
      set { this.SetPropertyValue( () => this.SelectedEntity, value ); }
   }

   public void Edit()
   {
      //perform work
   }
   
   public bool CanEdit
   {
      get{ return this.SelectedEntity != null; }
   }
}
```

In the above scenario whenever the `SelectedEntity` property changes a `INotifyPropertyChanged.PropertyChanged` event is raised also for the `CanEdit` property, thus WPF reevaluates the property and based on the boolean result the bound command will be enabled or not.

The markup extension is defined in the `http://schemas.radicalframework.com/windows/markup` xml namespace.
