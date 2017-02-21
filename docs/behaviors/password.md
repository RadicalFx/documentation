## Password

If we try to data-bind the Password property of a PasswordBox control we end up with an error because the Password property cannot be bound due to the fact that is not a dependency property (mainly for security reasons).

In those cases you can take advantage of the PasswordBoxBehavior in the following manner:

```xml
<PasswordBox>
    <i:Interaction.Behaviors>
        <behaviors:PasswordBoxBehavior Text="{Binding Path=MyPasswordProperty}" />
    </i:Interaction.Behaviors>
</PasswordBox>
```

where the behaviors xml namespace is defined as: `http://schemas.topics.it/wpf/radical/windows/behaviors`. In the same namespace is also defined a `Password` attached property that exposes exactly the same behaviors.

The password behavior, and the attached property too, exposes also a `Command` property useful to bind a command to the “enter” key when the `PasswordBox` is focused.