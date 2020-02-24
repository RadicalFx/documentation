## Manage focus

In the Model View ViewModel world there are a lot of things that can be considered borderline, focus management is one of those things. On the other hand managing focus in a desktop application based on WPF is other then a trivial task, focus in desktop application has many facets and lots of corner cases that must be taken into account, there is logical focus, keyboard focus and input scopes that determine the focus behavior.

[Radical](https://github.com/RadicalFx/radical) supports a basic focus management, where input scopes are not supported, and logical focus and keyboard focus are supported assuming they are always related to the same control at the same time.

## The View

In order to manage focus on the view side we need to introduce on each control we want to participate the following behavior:

```xml
<TextBox Margin="10" Text="{markup:EditorBinding Path=SampleText}">
    <i:Interaction.Behaviors>
        <lb:Focus ControlledBy="{Binding Path=FocusedElementKey}" UsingKey="SampleText" />
    </i:Interaction.Behaviors>
</TextBox>
```

where the "lb" xml namespace is defined in the `http://schemas.topics.it/wpf/radical/windows/behaviors` XML namespace:

The Focus behavior defines which property of the ViewModel controls the focused element (`ControlledBy`) and which is the key (`UsingKey`) that uniquely identifies the control among all the others, in this case we are using, as key, exactly the same name name as the property the control is bound to: "SampleText".

## The ViewModel

On the ViewModel side, if we inherit from the base [AbstractViewModel](../mvvm/abstract-view-model.md), we just need to decide which should be the “focused” key:

```csharp
class MainViewModel : AbstractViewModel
{
    public void SetFocus() 
    {
        this.MoveFocusTo(() => this.SampleText);
    }

    public String SampleText
    {
        get { return this.GetPropertyValue(() => this.SampleText); }
        set { this.SetPropertyValue(() => this.SampleText, value); }
    }
}
```

At runtime each time we call `MoveFocusTo`, exposed by the base view model we inherit from, the focus is moved to the UI element identified by the given key.
