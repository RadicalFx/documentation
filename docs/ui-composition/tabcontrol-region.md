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
             xmlns:rg="http://schemas.radicalframework.com/windows/presentation/regions"
             rg:RegionHeaderedElement.Header="This will be used as header">
</UserControl>
```

The header is not constrained to be a string but can be any valid XAML content.