## Create a custom region

Radical out of the box offers a limited set of regions: 

* ContentPresenterRegion;
* PanelRegion;
* TabControlRegion;

Building a custom region is a simple task, the first thing is to decide which type of region we need, depending on 3 factors:

* Single content vs multiple contents in a region;
* If we need multiple contents the next decision is if we need to have an active content, such as a `TabItem` in a `TabControl` or not;

### Menu and MenuItem regions

In a plugin based application is quite common to have the requirement to allow plugins to inject menus and menu items into the application main shell.
The easiest way to achieve it is to build custom regions capable of hosting Menus and MenuItems.
A region is bound the the XAML element where the `Region` attached property is defined. In a menu we have 2 element types the menu that hosts top level items, and menu items that can have children.

In order to host menu items in a menu via region we can simply use the following code:

```csharp
public class MenuRegion : ElementsRegion<Menu>
{
    public MenuRegion()
    {

    }

    public MenuRegion( String name )
    {
        this.Name = name;
    }

    protected override void OnAdd( DependencyObject view )
    {
        this.Element.Items.Add( ( MenuItem )view );
    }

    protected override void OnRemove( DependencyObject view, RemoveReason reason )
    {
        view.As<MenuItem>( e =>
        {
            if ( this.Element.Items.Contains( e ) )
            {
                this.Element.Items.Remove( e );
            }
        } );
    }
}
```

The important pieces are the `OnAdd` and the `OnRemove` protected methods. Since we are inheriting from a region whose element type is a `Menu` we have an`Element` property vailable that exposes to the region the XAML element the region is bound to, in the above sample the `Menu`.
`OnAdd` will be called by the infrastructure whenever there is the need to add a content to the region and `OnRemove` whenever there is the need to remove a content.
In the above sample we are simply adding and removing the element from the `Menu` that is hosting us. Following the same approach as above we can define a `MenuItemRegion`:

```csharp
public class MenuItemRegion : ElementsRegion<MenuItem>
{
    public MenuItemRegion()
    {

    }

    public MenuItemRegion( String name )
    {
        this.Name = name;
    }

    protected override void OnAdd( DependencyObject view )
    {
        this.Element.Items.Add( ( MenuItem )view );
    }

    protected override void OnRemove( DependencyObject view, RemoveReason reason )
    {
        view.As<MenuItem>( e =>
        {
            if ( this.Element.Items.Contains( e ) )
            {
                this.Element.Items.Remove( e );
            }
        } );
    }
}
```

that follows the exact same approach as the `MenuRegion`.

### Usage

Once a region is defined its usage is very simple:

```xaml
<Menu rg:RegionService.Region="{crg:MenuRegion Name=MainMenuRegion}">
    <MenuItem Header="File">
        <MenuItem Header="Exit">

        </MenuItem>
    </MenuItem>
</Menu>
```

### Adapters

One important thing to underline looking at the above samples is that a region is bound to an element type but not to a content type, this is in line with the overall XAML philosophy. This means that in the region itself we can adapt the incoming content in order to host it in the best possible way. In the above samples we are expecting the incoming content to be a `MenuItem` but nothing prevents us, as the `TabControlRegion` does, to change the behavior of the region based on the incoming content type. In te above sample what we can do is accept as content every `DependencyObject` and if it is not a valid `MenuItem` wrap it n a `MenuItem` before adding it as content.