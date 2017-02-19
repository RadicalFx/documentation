## Unity

In order to setup your app using Unity it’s enough to add a reference to `Radical.Windows.Presentation.Unity2` (or Unity3) via [nuget](http://nuget.org/) and configure the application like in the following snippet:

```csharp
sealed partial class App : Application
{
    ApplicationBootstrapper bootstrapper;
        
    public App()
    {
        this.bootstrapper = new UnityApplicationBootstrapper<Presentation.MainView>();
    }
}
```

for a detailed explanation of what’s going on take look at the Quick Start.
In the case you need to register your own components in Unity and the provided Bootstrap conventions does not satisfies your requirements you can leverage the power of Unity installers and MEF, drop a class like the following in your assembly:

```csharp
public class DefaultInstaller : IUnityInstaller
{
    public void Install( IUnityContainer container, BootstrapConventions conventions, IEnumerable<Types> allTypes )
    {
        //register your components here.
    }
}
```

your installer will be automatically wired up at boot time by the infrastructure.

if, for some reason, in your components you need a dependency on the container you can add a dependency directly on IUnityContainer or on the lightweight IServiceProvider, they are both automatically registered as singleton at boot time.