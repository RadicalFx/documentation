## Expose services as resources

Radical registers, during the application boot phase, all dependencies as components in the IoC container. Other components can depend on registered dependencies via DI. There are scenarios when DI is not available, for example when using WPF template selectors, and the code needs a dependency that is registered in the IoC container.
For these scenarios, it's possible to expose registered components as resources both at the application level or at the view level.
To expose a service at the application level, the following API can be used:

```
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<Presentation.MainView>(configuration => 
        {
            configuration.ExposeServiceAsResource<MyServiceType>();
        });
    }
}
```

If a service needs to be exposed only in the resources of a specific View, this other API can be used:

```
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<Presentation.MainView>(configuration => 
        {
            configuration.ExposeServiceAsResource<MyServiceType, MyView>();
        });
    }
}
```

It's possible to change the way resource keys are generated for exposed services via the `GenerateServiceStaticResourceKey` [convention](runtime-conventions.md)

NOTE: Be sure to not expose transient components as this might change their expected life-cycle.
