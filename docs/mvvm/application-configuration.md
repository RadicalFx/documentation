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

### Bootstrap conventions

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
