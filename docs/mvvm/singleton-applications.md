## Singleton applications

There are cases in which we need that our application cannot be started twice by the user, these applications are called singleton applications. We can use the really powerful Radical Presentation application bootstrapper to create a singleton application:

```csharp
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<Presentation.MainView>(configuration=>
        {
           configuration.RegisterAsLocalSingleton("my-singleton-key");
        });
    }
}
```

Using the `RegisterAsLocalSingleton` method we can set the singleton key (that in the end is the name of the Mutex used to handle “singletoness”) and make so the application is a singleton for the current user session. To make the application singleton globally for the running OS independently of the user (Global) use the `RegisterAsGlobalSingleton` method.

If the system determines that the application can run we have the opportunity to change this decision:

```csharp
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<Presentation.MainView>(configuration=>
        {
           configuration.RegisterAsLocalSingleton("my-singleton-key");
           configuration.OnSingletonApplicationStartup(e =>
           {
               e.AllowStartup = false;
           });
        });
    }
}
```

We can use the same exact approach as above to handle the case in which the application is starting and another instance is already running, in this case the value of the `AllowStartup` property is false, indicating that another instance is running.
