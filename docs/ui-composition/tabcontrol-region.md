## TabControl region

The `TabControlRegion` is a standard `switching elements region` that implements the adapter pattern in order to allow the user to add as `content` every XAML content.

The XAML `TabControl` element expects its children to be `TabItem` this is, from the user perspective, very uncomfortable.

It is much easier to deal with a standard `DependencyObject` and expect to be able to add that object as a `TabItem`. The `TabControlRegion` allows us to achieve that.

Allowing us to add a `DependencyObject`, such as a `UserControl`, as the content of a `TabControlRegion` solves only one the issues we have when using a `TabControl`.
A `TabControl` is what we call a `headered` element meaning that each `TabItem` is composed by 2 different pieces: the `TabItem` content and the `TabItem` header.
The `DependencyObject` we can add as content will be used as the `TabItem` content, in order to define the `TabItem` header we can use the `RegionHeaderedElement.Header` attached property, whose content will be used by the `TabControlRegion` as the `TabItem` header, such as in the following sample:

```xaml
<UserControl x:Class="SampleView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:rg="http://schemas.radicalframework.com/windows/presentation/regions">
             <rg:RegionHeaderedElement.Header>
                <TextBlock Text="This will be used as header" />
             </rg:RegionHeaderedElement.Header>
   <!-- user control content here -->
</UserControl>
```

The header is not constrained to be a string but can be any valid XAML content.

### Header data context

The WPF `TabItem` element has a `Header` and a `Content` property. The value of the `RegionHeaderedElement.Header` attached property is added the `TabItem.Header` property, this means that by default in the generated visual tree the tab item content and the tab item header don't share the same `DataContext`. Assuming a `TabView` view and a corresponding `TabViewModel` the following code snippet won't work as expected:

```xaml
<UserControl x:Class="TabView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:rg="http://schemas.radicalframework.com/windows/presentation/regions">
             <rg:RegionHeaderedElement.Header>
                <TextBlock Text="{Binding Path=PropertyOnTabViewMoodel}" />
             </rg:RegionHeaderedElement.Header>
   <!-- user control content here -->
</UserControl>
```

At runtime Radical creates a `TabViewModel` instance and binds it to the `TabView` UserControl. When the user control is added as content to the TabItem the header is extracted from the attached property, at this point the TabItem header DataContext is inherited from the TabControl DataContex and the `PropertyOnTabViewMoodel` property won't be found.
To make so that the header content has the same DataContext as the user control the `PreserveOwningRegionDataContext` attached property can be used:

```xaml
<UserControl x:Class="TabView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:rg="http://schemas.radicalframework.com/windows/presentation/regions"
             rg:RegionHeaderedElement.PreserveOwningRegionDataContext="true">
             <rg:RegionHeaderedElement.Header>
                <TextBlock Text="{Binding Path=PropertyOnTabViewMoodel}" />
             </rg:RegionHeaderedElement.Header>
   <!-- user control content here -->
</UserControl>
```

At runtime in the visual tree the TabItem header will have the user control DataContext. For backward compatibility reasons the default value of `PreserveOwningRegionDataContext` is `false`.
