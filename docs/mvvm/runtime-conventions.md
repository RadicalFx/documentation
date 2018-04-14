---
layout: default
toc: mvvm-toc.html
---
## Runtime conventions

`Radical.Windows.Presentation` has a lot of runtime conventions mainly related to two different areas:

* View – ViewModel relation;
* UI Composition;

Runtime conventions are managed by the `IConventionsHandler` interface, and allows to take full control of the following Radical behaviors:

* **ResolveViewModelType**: The first convention is used internally by the ViewResolver and given the view type returns the ViewModel type for the given view, the default behavior is that the view model is in the same namespace of the view and has the same type name suffixed with “Model” (e.g.: MainView and MainViewModel).

* **ResolveViewType**: The ResolveViewType convention is currently under development and not used, but basically does the opposite stuff, using the same default behavior, as the ResolveViewModelType convention. The toolkit utilizes a view first approach, thus resolving the view type given the view model type is not required, you can use this convention to implement a view model first based approach.

* **ViewReleaseHandler**: the `ViewReleaseHandler` is called each time a `View` should be released, this handler is responsible to release the `View` and its associated `ViewModel` if any. This handler also unsubscribe, if allowed by the `ShouldUnsubscribeViewModelOnRelease` convention, the `ViewModel` from all the subscriptions registered with the `MessageBroker`.

* **ShouldReleaseView**: determines if a `View` should be released when required.

* **ShouldUnsubscribeViewModelOnRelease**: determines if `ViewModel` subscriptions should be unsubscribed at release time.

* **ShouldUnregisterRegionManagerOfView**: internally used by the UI Composition engine to determine if a region manager should be destroyed when the owner `View` is released, the default behavior is to destroy region managers only if the `View` is not a singleton view.

* **FindHostingWindowOf**: This convention is currently used to find the Window that hosts a given view model. It is pretty useful to get a reference to the Window object that hosts, in its visual tree a View Model data bound to a UserControl.

    This task is performed finding the current view of the given view model (using another convention) and then reverse walking the visual tree looking for the first Window object.  
The convention accomplish two needs:

    1. A view model can implement the IExpectViewClosingCallback and the IExpectViewClosedCallback (and other *Callback(s)) in order to intercept the fact that the hosting Window is closing or has been closed and since we support UI Composition features a view model can be a view model attached to a UserControl that is runtime “inserted” into the visual tree of an existing Window;
    2. The UI Composition region service, in order to satisfy the above requirement, each time setups a new region need to find the hosting window in order to attach the closing and closed events;

* ViewModels as resources: there are scenarios in which it's handy to have the current `View` `ViewModel` available in the `View` resources. **ShouldExposeViewModelAsStaticResource** and **ExposeViewModelAsStaticResource** control if a `ViewModel` is exposed as a resource (`false` by default) and how it is exposed. The default behavior, when this feature is active, is to register the `ViewModel` in the resources using its `Type` name as the resource key.

* **ViewHasDataContext**, **SetViewDataContext** and **GetViewDataContext**: The ViewHasDataContext convention simply checks if the given view DataContext property is not null, this convention accepts a DependencyObject because in WPF the DataContext property is not defined on a single root object but is defined on FrameworkElement and on FrameworkContentElement.  
SetViewDataContext and GetViewDataContext respectively sets and gets the DataContext of the given view.

    *Note*:

    > The `ViewDataContextSearchBehavior` has been introduced to overcome an issue encountered due to the way dependency property value inheritance works. When using nested views, for example because one child view is loaded as a content injected into a region, if the nested view does not have a DataContext property (e.g. is a view without a ViewModel) its DataContext property value is inherited from the first element in the logical tree that has a DataContext assigned. This default WPF behavior was causing some subtle bugs in the way the Radical MVVM logic was working. The ViewDataContextSearchBehavior has been introduced to determine the way the MVVM engine will look for the ViewModel on a View, the default behavior, that can be controlled via the `DefaultViewDataContextSearchBehavior` property, is to look only on the View DataContext property ignoring each inherited value.

* **ShouldNotifyViewLoaded** This convention is responsible to determine if a `View` should notify that is loaded  broadcasting a `ViewLoaded` message. A view notifies that has been loaded in 2 cases:

     * If the View contains a `Region`;
     * If the View, or the associated ViewModel, is decorated with the `NotifyLoadedAttribute`;

    The same logic applies to the **ShouldNotifyViewModelLoaded** convention for the ViewModel.

* **AttachViewToViewModel** and **GetViewOfViewModel**: Internally the `Radical.Windows.Presentation` MVVM and UI Composition toolkit needs to know the runtime View – ViewModel relations in order to know that given a ViewModel instance the corresponding View instance is certainly a specific instance.
    
    To achieve that the `ViewResolver` once has resolved both the required instances calls the `AttachViewToViewModel` convention in order to store the view reference in the view model instance (the view model is already stored in view instance using the `DataContext` property).

    By design this works out-of-the-box because the `AbstractViewModel` type implements the `IViewModel` interface that has a `View` property internally used for this tasks. If the user does not like this behavior or cannot inherit from the `AbstractViewModel` type, nor implement the `IViewModel` interface, can replace this convention in order to store somewhere else the required relation (e.g. a statically defined dictionary).

    The same logic is used by the `GetViewOfViewModel` convention that is required to retrieve the stored relation.

* **TryHookClosedEventOfHostOf**: This is internal and is used by the region service engine to attach the closed event of the hosting window, if any, in order to cleanup stuff when the window is closed.

* **IsHostingView**: The `IsHostingView` convention is internally used by the `Region` base class to determine if a given visual element can be considered a View.

* **AttachViewBehaviors**: The AttachViewBehaviors convention can be hooked by the framework user if there is a requirement to attach behaviors (`System.Windows.Interactivity.Behavior<T>`) whenever a view is resolved by the ViewResolver. By default the built-in `ViewResolver` attaches the following behaviors to each view:

    * WindowLifecycleNotificationsBehavior;
    * FrameworkElementLifecycleNotificationsBehavior;
    * DependencyObjectCloseHandlerBehavior;
