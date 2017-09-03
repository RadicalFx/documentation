## UI Composition

Radical offers a fully flagged UI Composition engine based on the concept of regions.

> A `UI Composition` sample is available in the [Radical-Samples repository](https://github.com/RadicalFx/documentation/tree/master/samples).

## Concepts

A `region` is a named injectable portion of the UI where other components can inject their on content. A region is *attached* to a `DependencyObject` on the UI, depending on the type of the object the region is attached to the region behavior changes. Radical has 3 different main region types:

* `IContentRegion<T>`: a content region is thought for a `ContentPresenter` or a `ContentControl` UIElement, it can host one single content at a time and each time a new content is set the previous one will be removed;
* `IElementsRegion<T>`: an elements region can host multiple contents at a time, it is thought for a `Panel` UIElement, so each WPF control that inherits from panel, such as the `StackPanel`, can be used with an `IElementsRegion`; Content from an `IElementsRegion` can be added or removed and will be available depending on the logic implemented by the underlying UIElement;
* `ISwitchingElementsRegion<T>`: a switching elements region is an element region that, other than being able to host multiple elements at a time, has also the concept of an active element that can change over time; a typical sample is a `TabControl` where each `TabItem` can be seen;

*Note*: each time a content is removed from a region its lifecycle is managed as every View/ViewModel:

* View and ViewModel will be released;
* If View or ViewModel implements `IDisposable` they will be disposed;
* If View or ViewModel implements `IExpectViewClosedCallback` they will receive a callback notification;

Each region is characterized by 2 main attributes:

* is owned by a Region Manager, an `IRegionManager` implementation;
* has a unique name in the set of regions owned by the same Region Manager;

A `RegionManager` is automatically created by the UI Composition engine as soon as a region is added to a `View`, a `RegionManager` is bound to a WPF `Window` instance.

### Nesting

Regions can be nested as preferred, a Window can contain a region that at runtime will contain another region and so on without limitations. For example the following is a valid `logical tree`:

	Window
	  -> Grid
	     -> ContentPresenter
	        -> IContentRegion<ContentPresenter>
	          -> UserControl
	            -> Grid
	              -> StackPanel
	                -> IElementsRegion<StackPanel>

In the above sample one single RegionManager will be created at runtime. 

## Region Setup

### Region markup definition

First define a region in the XAML where is needed and attach it to the `UIElement` that requires injection:

```xaml
<Window x:Class="Samples.Presentation.MyView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:rg="http://schemas.topics.it/wpf/radical/windows/presentation/regions"
             xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"
             mc:Ignorable="d" 
             d:DesignHeight="300" d:DesignWidth="300">
    <Grid>
        <ContentPresenter rg:RegionService.Region="{rg:ContentPresenterRegion Name=MyRegion}" />
    </Grid>
</Window>
```

**Remarks**

* The `rg` namespace declaration pointing to the Radical region namespace `http://schemas.topics.it/wpf/radical/windows/presentation/regions`;
* A region is attached to a `UIElement` via the `Region` attached property of the `RegionService` element;
* A region is declared as a markup extension whose primary role is to define the region type and the region name;

As soon as we define a region the UI Composition engine, at runtime, will create a `RegionManager` to host the region, RegionManager whose lifecycle is **bound** to the lifecycle of the hosting `Window`. If the region is defined in a `UserControl` the RegionManager lifecycle will be **bound** to the lifecycle of the `Window` hosting the UserControl.

### Region Injection

Once a region is defined in the XAML we need to inject some content, we can inject content in a region in 3 different ways: manually, using partial views or using a declarative approach.

#### Manual injection

Once a View contains a region each time the View is loaded a `ViewLoaded` message is broadcasted to notify that the View has been loaded:

```csharp
class MyViewLoadedHandler : MessageHandler<ViewLoaded>, INeedSafeSubscription
{
	public IViewResolver ViewResolver{ get; set; }
	public IConventionsHandler Conventions{ get; set; }
	public IRegionService RegionService{ get; set; }

	protected override bool OnShouldHandle( ViewLoaded message )
	{
		return message.View is Samples.Presentation.MyView;
	}

	public override void Handle( ViewLoaded message )
	{
		if ( this.RegionService.HoldsRegionManager( message.View ) )
		{
			var view = this.viewResolver.GetView<MyRegionView>();

			var region = this.RegionService.GetRegionManager( message.View )
				.GetRegion<IContentRegion>( "MyRegion" );

			region.Content = view;
		}
	}
}
```

In the above sample we are defining a message handler to handle the `ViewLoaded` message, overriding the `OnShouldHandle` method to define a rule to handle only the ViewLoaded event related to the View we are interested in.

In the `Handle` method we utilize:

* the `RegionService` to determine is the View has a `RegionManager`;
* if the View has a region manager
    * we resolve the content to inject;
    * retrieve a reference to the region manager and to the region;
    * inject the content;

Resources:

* [Radical built-in messages](/mvvm/built-in-messages.md)
* [Runtime conventions](/mvvm/runtime-conventions.md)

#### Automatic (aka Partial regions)

Radical UI Composition engine has a concept called `partial view`, a partial view is a `View`, and if defined its `ViewModel`, that can be automatically picked up and injected based on a set of conventions:

* Given a region, as in the previous XAML sample named `MyRegion`;
* Given a View, and an optional ViewModel, that lives in a namespace that matches `*.Presentation.Partial.*`;
* Where the last segment of the View/ViewModel namespace is the region name, in our sample MyRegion;

The View will be resolved, as usual, and injected into the expected region. Given the following namespace structure:

	MySampleApp
	  .Presentation
	     .Partial
	        .MyRegion
	            .MySampleView.xaml
	            .MySampleViewModel.cs

The MySampleView and it ViewModel, MySampleViewModel, will be automatically injected into the MyRegion region.

#### Declarative

The last option, to inject a `View` in a specific `region`, is to decorate the `View` class with the `InjectViewInRegionAttribute`:

```csharp
[InjectViewInRegion( Named = "MyRegion" )]
class MyUserControlView : UserControl
{

}
```

In the above sample, at runtime, the UI Composition engine will inject an instance of the `MyUserControlView` into the region named "MyRegion".

## Region implementations

As previously said Radical has 3 different region types: `IContentRegion<T>`, `IElementsRegion<T>` and `ISwitchingElementsRegion<T>`. Each region type has a default implementation.

### ContentPresenterRegion

A `ContentPresenterRegion` is a `IContentRegion<ContentPresenter>` that can be applied to a `ContentPresenter UIElement`.

### PanelRegion

A `PanelRegion` is a `IElementsRegion<Panel>`, given that a `Panel` is an abstract class, this region can be used with any `UIElement` that inherits from `Panel`, such as a `StackPanel`.

### TabControlRegion

A `TabControlRegion` is an implementation of the `ISwitchingElementsRegion<TabControl>` and can be used with a `TabControl`.
