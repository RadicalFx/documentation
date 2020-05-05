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

Bootstrap conventions are designed to configure the Radical application boostrap phase and the IoC/DI setup. Bootstrap conventions customization happens throughout the 

```
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<MainView>(configuration => 
        {
           configuration.BootstrapConventions.IsViewModel = type => 
           {
              if (type.Namespace == "MyViewModelsNamespace") 
              {
                 return true;
              }

              return configuration.BootstrapConventions.DefaultIsViewModel(type);
            };
         });
    }
}
```

### Assembly scanning

### ExposeServiceAsResource

### Culture and UI Culture

### Shell type

### OverrideShutdownMode

### Singletons

### Auto boot

### Intercept boot/shutdown lifecycle

### Spalsh screen

### Unhandled exceptions
