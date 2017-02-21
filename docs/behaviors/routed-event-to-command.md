## Generic routed event handler to command behavior

### Scenario

Imagine that you need to handle from the ViewModel the `SelectedIndexChanged` of a WPF `TreeView`, currently the only way (without using any particular framework) is to build your own behavior to achieve that, or bind, via a style the `IsSelected` property of the node to a property of the view model, but in this second case the side effect is that to find the selected item you need to visit the whole tree.

### Radical “Handle”

```xml
<TreeView Margin="5" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" 
                     ItemsSource="{Binding Path=MyListOfElements}">
    <i:Interaction.Behaviors>
        <behaviors:Handle RoutedEvent="TreeView.SelectedItemChanged"
                          WithCommand="{Binding Path=MyAmazingCommand}"
                          PassingIn="$args.NewValue" />
    </i:Interaction.Behaviors>
    <TreeView.ItemTemplate>
        <!-- omitted -->
    </TreeView.ItemTemplate>
</TreeView>
```

As you can imagine the value of the `NewValue` property of the event arguments is passed as the command parameter to the command, we currently support as placeholder:

* `$args`: the routed event arguments;
* `$this`: the WPF element the behavior is attached to;
* `$source`: the source of the routed event;
* `$originalSource`: the original source of the routed event;

Obviously the event we need to handle must be a `RoutedEvent`.

The behavior is defined in the `http://schemas.topics.it/wpf/radical/windows/behaviors` xml namespace.