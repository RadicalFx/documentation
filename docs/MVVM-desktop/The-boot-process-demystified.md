What happens under the hood when we write this really trivial piece of code:

```csharp
public partial class App : Application
{
    public App()
    {
        var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>();
    }
}
```

As we have already seen in the [[quick start|Quick Start (WPF)]] we are doing 2 main choices:

* We are booting Castle Windsor as IoC container;
* We are declaring that the MainView window in the Presentation namespace is the MainWindow of our application;

Internally the application boot process is not so trivial as it appears from the outside, when the Startup event is raised by the application the bootstrapper:

**Creates the service provider**

That, in the end, is the inversion of control container of your choice.

This task is accomplished by the concrete bootstrapper overriding the abstract method “CreateServiceProvider()”, in the above sample by the WindsorApplicationBootstrapper that creates an instance of Windsor and returns it to the bootstrapper as a IServiceProvider;

**Create the MEF AggregateCatalog**

That can be used during the bootstrap process to aggregate application modules.

Accomplished directly by the ApplicationBootstrapper class virtual method “CreateAggregateCatalog( IServiceProvider )”, inheritors can override this method to customize the created catalog. By default the ApplicationBootstrapper adds to the AggregateCatalog 3 different DirectoryCatalog(s):

1. one directory catalog to match all the Radical*.dll assemblies;
2. one directory catalog to match all the {entry assembly name}*.dll assemblies;
3. one assembly catalog for the [entry assembly];

The last 2 catalogs ensures that all the assemblies that starts with the same name of the entry assembly will be analyzed by MEF.

If we need to change the above behavior there are 3 options:

1. Redefine the delegate used by the bootstrapper to identifies catalogs:

   ```csharp
   public partial class App : Application
   {    
       public App()    
       {        
           var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>();
           bootstrapper.DefineCatalogs = ()=> 
           {
               return new []{ new DirectoryCatalog( "[path]" ) };
           };
       }
   }
   ```

   we are completely redefining the whole content of the AggregateCatalog, since the DefineCatalogs is a Func<IEnumerable<ComposablePartCatalog>> we can partially override the delegate and remove some predefined catalogs, for example, and add some other;

2. On the other hand if we simply need to add some more catalogs to the default ones we can write the following code:

   ```csharp
	public partial class App : Application
	{    
		public App()    
		{        
			var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>();
			bootstrapper.OnCatalogDefinition( () => 
			{
				//we are adding a new catalog to the default ones.
				return new []{ new DirectoryCatalog( "[path]" ) };
			} );
		}
	}
	```

3. Finally we can change the BootstrapConventions to determine which are the patter that the directory catalogs should use to scan for assemblies;

**Creates an instance of the MEF composition container**

Once we have the previously created AggregateCatalog the bootstrapper creates an in instance of MEF via the “CreateCompositionContainer( AggregateCatalog, IServiceProvider )”.

**Compose**

As soon as the composition container is created the boot process composes itself against MEF and notifies inheritors that the composition process is completed calling the “OnCompositionContainerComposed( CompositionContainer, IServiceProvider )” virtual method. The default ApplicationBootstrapper class does nothing in that method, but, for example, the WindsorApplicationBootstrapper performs the Windsor container setup; the same approach is used by the PuzzleApplicationBootstrapper for WinRT.

*WindsorInstaller(s) and PuzzleSetupDescriptor(s)*

*[[Windsor container|Castle Windsor]] and [[Puzzle container|Puzzle Container]] both have support for installers, or descriptors in Puzzle terminology, we use MEF to compose descriptors at boot time and dramatically simplify the wire-up process required to setup the container. The following is an excerpt of the code used in the WindsorApplicationBootstrapper to setup the container:*

```csharp
[ImportMany]
IEnumerable<IWindsorInstaller> Installers { get; set; }

protected override void OnCompositionContainerComposed( CompositionContainer container, IServiceProvider serviceProvider )
{
	base.OnCompositionContainerComposed( container, serviceProvider );

	var toInstall = this.Installers.Where( i => this.ShouldInstall( i ) ).ToArray();

	if ( this.onBeforeInstall != null ) 
	{
		var conventions = this.container.Resolve<Boot.BootstrapConventions>();
		this.onBeforeInstall( conventions );
	}

	this.container.Install( toInstall );
}
```

As we can see all we do is to expose (private) an import-many property, at composition time MEF scans all the catalogs looking for types that export the IWindsorInstaller type and populate the import property, leaving us with the only trivial task to call a method on the container.

One important thing to notice is that here we have the ideal hook to change the boot conventions, we can write the following piece of code to be sure to change the conventions right before they are used:

```csharp
public partial class App : Application
{
	public App()
	{
		var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>();
		bootstrapper.OnBeforeInstall( conventions => 
		{
			//modify conventions behavior here.
		} );
	}
}
```

**Setup UI Composition Support**

After having boot the container the application bootstrapper takes care of configuring the UI Composition system, we’ll discuss what happens in the UI Composition chapter.

**ShutdownMode**

In WPF applications there is the concept of ShutdownMode the application bootstrapper does not change in any way the default value of the Application.Current.ShutdownMode unless explicitly requested by user:

```csharp
public partial class App : Application
{
    public App()
    {
        var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>();
        bootstrapper.OverrideShutdownMode( ShutdownMode.OnLastWindowClose );
    }
}
```

**Principal initialization**

Once the application services are setup the bootstrapper takes care of setting up the Thread.CurrentPrincipal, the default behavior is to use the current user Windows identity. This behavior can be changed in 2 different ways:

1. Inheriting from the bootstrapper and overriding the InitializeCurrentPrincipal virtual method;
2. Setting a different principal right after the boot process is completed, using the supplied hook;

**Culture & UICulture**

After setting up the principal and finally returning control to the application the boot process has the option to setup the Culture and the UICulture of the current Thread. The default behavior is to use values of the hosting OS. The default behavior can be overwritten in the following manner:

```csharp
public partial class App : Application
{
    public App()
    {
        var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>();
        bootstrapper.UsingAsCurrentCulture( () => 
        {
            return new CultureInfo( "it-IT" );
        } );

        bootstrapper.UsingAsCurrentUICulture( () =>
        {
            return new CultureInfo( "en-US" );
        } );
    }
}
```

**Boot**

Once everything is setup the bootstrapper gives us the ability to take part into the boot process before the main window is shown:

```csharp
public partial class App : Application
{
    public App()
    {
        var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>();
        bootstrapper.OnBoot( container => 
        {
            //the UI is not yet started
        } );
    }
}
```

**BootCompleted**

The last event in the process is the one used to show the main window, we have the opportunity to be notified using the exposed handler:

```csharp
public partial class App : Application
{
    public App()
    {
        var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>();
        bootstrapper.OnBootCompleted( container => 
        {
            //the UI is setup
        } );
    }
}
```

Some of the state of the boot process are also [[notified to the application using the message broker|Built in messages]].

**Intercepting unhandled exceptions**

if we need to be notified whenever an unhandled exception occurs in our application we can use the provided hook:

```csharp
public partial class App : Application
{
    public App()
    {
        var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>()
            .OnUnhandledException( e =>
            {

            } );
    }
}
```

**Handling the application Shutdown**

As for the startup we can also handle the shutdown process of the application:

```csharp
public partial class App : Application
{
    public App()
    {
        var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>()
            .OnShutdown( reason =>
            {
                //handle services/components shutdown here
            } );
    }
}
```

When the application shuts down the provided delegate is invoked passing in the reason why the application is shutting down:

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

As we can see we can easily determine why the application is shutting down. Currently there is no way from the application bootstrapper to cancel the shutdown process, in order to achieve that we need to subscribe to the ApplicationShutdownRequested message via the message broker.

Someone may have noticed that one of the shutdown reasons is MultipleInstanceNotAllowed, yes Radical can handle singleton application for us with minimal effort, take a look at the [[singleton applications|Singleton applications]] chapter.

The [[application shutdown|Application shutdown]] chapter discusses all the details of the shutdown process and how to control/invoke it.