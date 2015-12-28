The WPF `TextBox` that shipped with the .net 3.5 release had a bug, fixed in later versions, that prevents to set the `UndoLimit` property to 0, Radical provides a behavior as a workaround:

```xml
<TextBox Text="bla bla...">
    <i:Interaction.Behaviors>
        <behaviors:DisableUndoManagerBehavior />
    </i:Interaction.Behaviors>
</TextBox>
```

the behavior is defined in the `http://schemas.topics.it/wpf/radical/windows/behaviors` xml namespace.