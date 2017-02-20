## Singleton applications

There are cases in which we need that our application cannot be started twice by the user, these applications are called singleton applications. We can use the really powerful Radical Presentation application bootstrapper to create a singleton application:

```csharp
public partial class App : Application
{
    public App()
    {
        var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>()
            .RegisterAsSingleton( "my-singleton-key", SingletonApplicationScope.Local );
    }
}
```

Using the `RegisterAsSingleton` method we can set the singleton key (that in the end is the name of the Mutex used to handle “singletoness”) and we can specify if we want our application to be singleton in the current user session (Local) or globally for the running OS independently of the user (Global).
If the system determines that the application can run we have the opportunity to change this decision:

```csharp
public partial class App : Application
{
    public App()
    {
        var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>()
            .RegisterAsSingleton( "my-singleton-key", SingletonApplicationScope.Local )
            .OnSingletonApplicationStartup( e =>
            {
                e.AllowStartup = false;
            } );
    }
}
```

We can use the same exact approach as above to handle the case in which the application is starting and another instance is already running, in this case the value of the `AllowStartup` property is false, indicating that another instance is running.