## TextBox Command

It could be interesting to attach a command to a TextBox if the enter key is pressed while the TextBox is focused so to provide a better UX to the end user, the TextBoxManager Command attached property (or behavior) comes to the rescue in this scenario:

```xml
<TextBox Text="bla bla...">
    <i:Interaction.Behaviors>
        <behaviors:TextBoxCommandBehavior Command="{Binding Path=MyCommand}" />
    </i:Interaction.Behaviors>
</TextBox>
```

```xml
<TextBox Text="bla bla..." behaviors:TextBoxManager.Command="{Binding Path=MyCommand}" />
```

the attached property, and the behavior too, is defined in the `http://schemas.radicalframework.com/windows/behaviors` xml namespace.