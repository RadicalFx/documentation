## Application configuration

The Radical application behavior, the bootstrap and runtime conventions, and the assembly scanning behavior can be be tweaked by accessing the `BootstrapConfiguration` instance:

```csharp
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<MainView>(configuration=>
        {
           //use the configration instance
        });
    }
}
```

### Conventions

Conventions can be customized during the application setup phase, for more information about convention refer to the [conventions](conventions.md) section.

#### Bootstrap conventions

[Bootstrap conventions](bootstrap-conventions.md) are designed to configure the Radical application boostrap phase and the IoC/DI setup. Bootstrap conventions customization happens throughout the `BootstrapConventions` object exposed by the configuration instance.

### Assembly scanning

By default a Radical application scans all the assemblies found in the bin directory and in its subdirectories. It's possible to configure the assembly scanning behavior to customize how types are loaded and used to configure the IoC/DI infrastructure. Refer to the [boot process](boot-process.md) documentation for more information.

### ExposeServiceAsResource

Radical registers, during the application boot phase, all dependencies as components in the IoC container. Other components can depend on registered dependencies via DI. There are scenarios when DI is not available, for example when using WPF template selectors, and the code needs a dependency that is registered in the IoC container.
For these scenarios, it's possible to expose registered components as resources both at the application level or at the view level. For more information refer to the [Services as Resources documentation](services-as-resources.md).

### Singletons

There are cases in which we need that our application cannot be started twice by the user, these applications are called singleton applications. For more information on how to customize the bootstrapp process to handle such cases, refer to the [singleton applications documentation](singleton-applications.md).

### Spalsh screen

Radical has built-in support for splash screens. Refer to the [splash screen how to](../how-tos/splash-screen.md), for more details.

### Disable auto-boot

Radical applications automatically subscribe to the hosting WPF Application lifecycle events to bootstrap the Radical infrastructure. To gain full control over the boot steps use the `DisableAutoBoot()` configuration option and call `Boot()` when in need of boostrapping the Radical application.

NOTE: When using [generic host](../ioc/third-party.md) support `DisableAutoBoot()` is ignored as the generic host approach already gives full control over the bootstrap process.
