We have seen that the infrastructure has a way, by default based on behaviors, to notify a `ViewModel` that its own `View` state is changing.

**The View is a Window**

If the view is a window we have several state that can be handled/intercepted by the coupled `ViewModel`:

* Loaded;
* Activated;
* Shown;
* Closing;
* Closed;

**The View is a FrameworkElement** (e.g. a UserControl)

If the view is a user control the only state we can intercept is the `Loaded` event.