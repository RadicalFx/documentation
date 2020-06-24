## Application boot process demystified

What happens under the hood when we write this really trivial piece of code?

```csharp
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<MainView>();
    }
}
```

As we have already seen in the [quick start](/README.md#steps-to-bootstrap-your-project-in-3-minutes) we are doing 2 main choices:

* We boot using the default IoC container provided by `Microsoft.Extensions.DeendencyInjection`;
* We declare that the `MainView` window is the main/shell window of our application;

Internally the application boot process is not so trivial as it appears from the outside, when the `Startup` event is raised by the WPF application the bootstrapper:

### Identifies assemblies to scan

In order to configure the IoC containers assemblies needs to scanned to load types that need to be registered for DI. This is accomplished by the assembly scanner. It's possible to customize some of the assembly scanner behaviors by using the `AssemblyScanner` property of the `BootstrapConfiguration` instance, like in the following snippet:

```csharp
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<MainView>(configuration =>
        {
            var scanner = configuration.AssemblyScanner;
            scanner.DirectorySearchOptions = SearchOption.TopDirectoryOnly; //default value
        });
    }
}
```

#### Register additional types in the IoC container

To register custom types, other than the ones already automatically registered via bootstrap conventions, a dependency installer is required. Create a class that implements the `IDependencyInstaller` interface. A class instance will be automatically created at runtime and the `Install` method will be invoked:

```csharp
public class MyCustomInstaller : IDependencyInstaller
{
   public void Install(BootstrapConventions conventions, IServiceCollection services, IEnumerable<Type> assemblyScanningResults)
   {
      services.AddSingleton<MyCustomSingleton>();
   }
}
```

### Creates the service provider

Once assemblies and types are scanned and identified through bootstrap conventions the default IoC container provided by `Microsoft.Extensions.DeendencyInjection` is created. In case an instance of the created `IServiceProvider` is required outside the scope of the Radical application, it can be retrieved using the following snippet:

```csharp
public partial class App : Application
{
    public App()
    {
        IServiceProvider container = null;
        this.AddRadicalApplication<MainView>(configuration =>
        {
            configuration.OnBootCompleted(serviceProvider => container = serviceProvider);
        });
    }
}
```

### ShutdownMode

WPF applications have the concept of `ShutdownMode`. Application bootstrapper does not change in any way the default value of the `Application.Current.ShutdownMode` unless explicitly requested by user:

```csharp
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<MainView>(configuration =>
        {
            configuration.OverrideShutdownMode(ShutdownMode.OnLastWindowClose);
        });
    }
}
```

### Principal initialization

Once the application services are setup the bootstrapper takes care of setting up the `Thread.CurrentPrincipal`, the default behavior is to use the current user `Windows identity`. This behavior can be changed by setting a different principal right after the boot process is completed, using the `OnBootCompleted` handler;

### Culture & UICulture

After setting up the principal and finally returning control to the application the boot process has the option to setup the `Culture` and the `UICulture` of the current `Thread`. The default behavior is to use values of the hosting OS. The default behavior can be overwritten in the following way:

```csharp
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<MainView>(configuration =>
        {
           configuration.UseCulture(container=>new CultureInfo("it-IT"));
           configuration.UseUICulture(container=>new CultureInfo("it-IT"));
        });
    }
}
```

### Boot

Once everything is setup the bootstrapper gives us the ability to take part into the boot process before the main window is shown:

```csharp
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<MainView>(configuration =>
        {
           configuration.OnBooting(container=>
          {
             //boot is in progress, UI is not visible yet.
          });
        });
    }
}
```

### BootCompleted

The last event in the process is the one used to show the main window, we have the opportunity to be notified using the exposed handler:

```csharp
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<MainView>(configuration =>
    {
       configuration.OnBootCompleted(container=>
       {
          //UI is fully setup
       });
    });
    }
}
```

Some of the state of the boot process are also [notified to the application using the message broker](built-in-messages.md).

### Intercepting unhandled exceptions

if we need to be notified whenever an unhandled exception occurs in our application we can use the provided hook:

```csharp
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<MainView>(configuration =>
        {
        configuration.OnUnhandledException(ex=> { /* deal with exception */ });
        });
    }
}
```

### Handling the application Shutdown

As for the startup we can also handle the shutdown process of the application:

```csharp
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<MainView>(configuration =>
        {
            configuration.OnShuttingDown(args=> { });
        });
    }
}
```

When the application shuts down the provided delegate is invoked passing in wehter the boot was completed or not, and the reason why the application is shutting down:

```csharp
public enum ApplicationShutdownReason
{
    /// <summary>
    /// The application has been shutdown using the Radical canonical behaviors.
    /// In this case the shutdown process can be canceled.
    /// </summary>
    UserRequest = 0,

    /// <summary>
    /// The application is shutting down because another
    /// instance is already running and the application
    /// is marked as singleton.
    /// </summary>
    MultipleInstanceNotAllowed = 1,

    /// <summary>
    /// The application is shutting down because the operating system session is ending.
    /// </summary>
    SessionEnding,

    /// <summary>
    /// The application has been shut down using the App.Current.Shutdown() method.
    /// </summary>
    ApplicationRequest,
}
```

As we can see we can easily determine why the application is shutting down. Currently there is no way from the application bootstrapper to cancel the shutdown process, in order to achieve that we need to subscribe to the `ApplicationShutdownRequested` message via the message broker.

Someone may have noticed that one of the shutdown reasons is `MultipleInstanceNotAllowed`, Radical can handle singleton application for us with minimal effort, take a look at [singleton applications](singleton-applications.md).

[Application shutdown](application-shutdown.md) discusses all the details of the shutdown process and how to control/invoke it.
