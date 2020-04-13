## Third party containers

To enable third party containers support it is necessary to bootstrap the Radical application using the generic host support. To enable generic host support:

- Add a reference to the `Microsoft.Extensions.Hosting` nuget package
- Change the app boostrapping code as follows:

```csharp
class App
{
   public App()
   {
      var host = new HostBuilder()
         .AddRadicalApplication<Presentation.MainView>()
         .Build();

      Startup += async (s, e) => 
      {
         await host.StartAsync();
      };

      Exit += async (s, e) =>
      {
         using (host)
         {
            await host?.StopAsync();
         }
      };
   }
}
```

Using the above code sample the application boostrapping process is now delegated to the generic host. To add support for a different IoC container, for example Autofac, do the following:

- Add a reference to the `Autofac.Extensions.DependencyInjection` nuget package
- Change the `HostBuilder` creation section to add Autofac support as follows:

```
var host = new HostBuilder()
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .AddRadicalApplication<Presentation.MainView>()
    .Build();
```

Refer to the documentation of you container of choice to get an overview of the steps required to integrate with the generic host.
