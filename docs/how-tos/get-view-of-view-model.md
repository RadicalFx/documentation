There are cases where given a running ViewModel we need to retrieve an instance of the currently associated View, the runtime conventions object exposes a convention to achieve that:

```csharp
class MyViewModel : AbstractViewModel
{
    public MyViewModel(  IConventionsHandler conventions )
    {
        var view = conventions.GetViewOfViewModel( this );
    }
}
```

Since the conventions are handled by the underlying Inversion of Control subsystem we can access the conventions using a dependency, as in the above sample.

### Notes:

if we take a look at the signature (Func<Object, DependencyObject>) of the above convention we notice that the “in” parameter is an Object and is not constrained to be an AbstractViewModel, this is in line with the fact that for the Radical toolkit a ViewModel is not required to be an AbstractViewModel, but if you need to use the above convention or the UI Composition system be aware that if the ViewModel is not an AbstractViewModel you end up with 2 options:

* implement on your view model the IViewModel interface;
* or replace the AttachViewToViewModel convention that is responsible to reverse link the View to the ViewModel;