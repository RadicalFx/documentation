We have seen how Radical Presentation [[resolves views instances|The IViewResolver]] at runtime and we have told that during the resolution process we inject/attach to the resolved view some behaviors using a convention.

The convention attaches the following behaviors to each resolved view:

* to every view (every DependencyObject) attaches the “**DependencyObjectCloseHandlerBehavior**” whose role is to allow the view model to send a close request message to its own view without the need to handle a reference to the view;
* if the **view is a window** attaches the “**WindowLifecycleNotificationsBehavior**” used to notify to the view model the lifecycle state changes of the view (loaded, shown, activated, closing and closed);
* “else if” the **view is a FrameworkElement** attaches the “**FrameworkElementLifecycleNotificationsBehavior**” whose role is notify to the view model when the view is loaded;

The easiest way to handle view lifecycle state changes in the `ViewModel` is to setup a [[callback expectation|Callback expectations]].

#### Automatic broker unsubscribe

The `WindowLifecycleNotificationsBehavior` whenever the `view` is closed invokes `ViewReleaseHandler` convention that is responsible to determine if the `ViewModel` associated with the closed `View` should be unsubscribed from all the message broker subscriptions, if any, created. 