In order to shutdown an application built using Radical Presentation there are 3 main options.

**Canonical WPF way: App.Current.Shutdown();**

There is no reason to not use the default WPF standard way to shutdown the application, the only thing we cannot do in this case is to prevent the shutdown process to complete, we have no control over it.

When the App.Current.Shutdown() method is called the bootstrapper raises, via the message broker, the following events:

* ApplicationShutdown: that simply notifies to the application that is shutting down;

**2 way shutdown via ApplicationBootstrapper.Shutdown();**

If we need an option to cancel the application shutdown process we should use the Shutdown method exposed by the ApplicationBootstrapper. In this way the following events are broadcasted/dispatched by the message broker:

1. *ApplicationShutdownRequested* is dispatched synchronously to the application and has a Cancel property that can be set to true to cancel the shutdown process;
2. *ApplicationShutdownCanceled* is broadcasted whenever the shutdown process is cancelled;
3. *ApplicationShutdown* is finally dispatched asynchronously to notify to the application that is shutting down;

**2 way shutdown via ApplicationShutdownRequest message**

Exactly the same approach as above can be obtained broadcasting, via the message broker, the *ApplicationShutdownRequest* message, without the need to have a reference to the bootstrapper.