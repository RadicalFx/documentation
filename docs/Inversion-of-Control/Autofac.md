In order to setup your app using Autofac it’s enough to add a reference to `Radical.Windows.Presentation.Autofac` via [nuget](http://nuget.org/) and configure the application like in the following snippet:

	sealed partial class App : Application
	{
	    ApplicationBootstrapper bootstrapper;
	        
	    public App()
	    {
	        this.bootstrapper = new AutofacApplicationBootstrapper<Presentation.MainView>();
	    }
	}

for a detailed explanation of what’s going on take look at the Quick Start.
In the case you need to register your own components in Autofac and the provided Bootstrap conventions does not satisfies your requirements you can leverage the power of Autofac module installer and MEF, drop a class like the following in your assembly:

	public class DefaultModule : IAutofacModule
	{
	    public void Configure( ContainerBuilder builder, BootstrapConventions conventions, IEnumerable<Assembly> assemblies )
	    {
	        //register your components here.
	    }
	}

your installer will be automatically wired up at boot time by the infrastructure.

if, for some reason, in your components you need a dependency on the container you can add a dependency directly on `IContainer` or on the lightweight `IServiceProvider`, they are both automatically registered as singleton at boot time.