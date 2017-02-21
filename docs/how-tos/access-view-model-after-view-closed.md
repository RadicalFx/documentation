## Accessing the view model after the view is closed

Good or not one of the scenario users need to support is to be able to use a `ViewModel` after the hosting `View` is closed. A typical sample is the following:

```csharp
var view = viewResolver.GetView<MySampleView>();
view.ShowDialog();
var viewModel = conventions.GetViewDataContext( view, ViewDataContextSearchBehavior.LocalOnly );
//access here some properties of the viewModel instance
```

The above snippet will fail with a Null Reference Exception due to the fact that the `viewModel` instance will be null. The underlying reason is that as soon as the hosting view is closed:

* the `ViewModel` is detached from its `View`;
* Both components are released through the `IReleaseComponents` service, causing disposition of disposable instances;

Given the above it is by design that `GetViewDataContext` will return a null reference in the above scenario. A first approach, as a workaround, can be the following:

```csharp
var view = viewResolver.GetView<MySampleView>();
var viewModel = conventions.GetViewDataContext( view, ViewDataContextSearchBehavior.LocalOnly );
view.ShowDialog();
//access here some properties of the viewModel instance
```

We simply retrieve a reference to the `ViewModel` before the view is closed and we use it later. What can happen is that accessing one of the ViewModel properties an `ObjectDisposedException` is raised because the `AbstractViewModel` base class knows that the `ViewModel` instance was released and disposed.

The definitive solution is to override the automatic release behavior decorating the `View` class with the `ViewManualReleaseAttribute` and changing our code as follows:

```csharp
var view = viewResolver.GetView<MySampleView>();
view.ShowDialog();
var viewModel = conventions.GetViewDataContext( view, ViewDataContextSearchBehavior.LocalOnly );
//access here some properties of the viewModel instance
conventions.ViewReleaseHandler( view, ViewReleaseBehavior.Force );
```

Once `MySampleView` is decorated with the `ViewManualReleaseAttribute` it won't be released anymore automatically and the `ViewModel` won't be detached, once we have finished using the `ViewModel` instance we ask to the conventions to force the release of the `View` and of its `ViewModel` using the `ViewReleaseHandler` convention and the `Force` enumeration value to override the default behavior.