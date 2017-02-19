## AbstractViewModel

When dealing with MVVM and ViewModel(s) there are a lot of things that a base class, such as the AbstractViewModel, can do for us in order to reduce the friction of the daily work.

The AbstractViewModel can (it is not required, even if is highly suggested) be used as a base class for all the application ViewModel(s), defining a view model is as easy as:

```csharp
class MainViewModel : AbstractViewModel
{

}
```

Nothing special, a simple and trivial class that inherits from the base AbstractViewModel type.

For the Radical toolkit a ViewModel is not required to be an AbstractViewModel, but if you do not to use the AbstractViewModel class as a base class for all the ViewModels you end up with 2 options:

* implement on your view model the IViewModel interface;
* or replace the AttachViewToViewModel convention that is responsible to reverse link the View to the ViewModel;

As soon as we do that we gain some benefits:

**Property change notification**:

the obvious benefit is that we immediately get INotifyPropertyChanged support:

```csharp
private String _text;

public String Text
{
    get { return _text; }
    set 
    {
        _text = value;
        this.OnPropertyChanged( () => this.Text );
    }
}
```

But given that writing properties in such a verbose way is a waste of time we can leverage the power of the Property System

**Radical Property System**:

The above property can be written in the following manner without altering the behavior:

```csharp
public String Text
{
    get { return this.GetPropertyValue( () => this.Text ); }
    set { this.SetPropertyValue( () => this.Text, value ); }
}
```

But the property system is not limited to changes notification, we can for example do the following:

```csharp
class MainViewModel : AbstractViewModel
{
    public MainViewModel()
    {
        this.GetPropertyMetadata( () => this.Text )
            .AddCascadeChangeNotifications( () => this.Sample );
    }

    public String Text
    {
        get { return this.GetPropertyValue( () => this.Text ); }
        set { this.SetPropertyValue( () => this.Text, value ); }
    }

    public Int32 Sample
    {
        get { return this.GetPropertyValue( () => this.Sample ); }
        set { this.SetPropertyValue( () => this.Sample, value ); }
    }
}
```

we have defined 2 properties and we are chaining the properties change notification in order to notify a change to the Sample property each time the Text property changes.  