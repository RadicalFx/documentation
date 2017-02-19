## Castle Windsor

As we have already seen in the [[quick start|Quick Start (WPF)]] we provide a default implementation of the IoC support using Castle Windsor as IoC container, this implementation is pluggable and completely based on conventions meaning that in most cases you do not need to interact directly with Windsor.

In order to setup the applications using Windsor it’s enough to add a reference to `Radical.Windows.Presentation.CastleWindsor` via [nuget](http://nuget.org/) and configure the application like in the following snippet:

```csharp
public partial class App : Application
{
    public App()
    {
        var bootstrapper = new WindsorApplicationBootstrapper<MainView>();
    }
}
```

In the case you need to register your own components in Windsor and the provided Bootstrap conventions does not satisfies your requirements you can leverage the power of Windsor Installers and MEF, drop a class like the following in your assembly:

```csharp
[Export( typeof( IWindsorInstaller ) )]
public class DefaultInstaller : IWindsorInstaller
{
    public void Install( IWindsorContainer container, IConfigurationStore store )
    {
        //register your components here
    }
}
```

your installer will be automatically wired up at boot time by the infrastructure.

if, for some reason, in your components you need a dependency on the container you can add a dependency directly on IWindsorContainer or on the lightweight IServiceProvider, they are both automatically registered as singleton at boot time.