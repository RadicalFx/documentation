## Inversion of Control

[Radical](https://github.com/RadicalFx/radical) Presentation toolkit is completely based on Inversion of Control and Dependency Injection principles but does not force the end user to use any predefined IoC toolkit.

Using a IoC framework is not a requirement at all although some default services implementation relies on the IServiceProvider interface \(that exists in the .net framework since v1\).

Using a IoC framework is, on the other hand, highly suggested since that the benefit and simplification introduced in the application management greatly overlaps the learning curve of the introduction of the IoC container.

By default the `Microsoft.Extensions.DependencyInjection` buil-in container is used. It's possible to use any container that supports the `IServiceProviderFactory<TContainerBuilder>` interface, for example to use `Autofac` the following code can be used:

```
public partial class App : Application
{
   public App()
   {
      var serviceProviderFactory = AutofacServiceProviderFactory(containerBuilder => 
      {
         //configure the builder here
      });
      var bootstrapper = new ApplicationBootstrapper<Presentation.MainView>(serviceProviderFactory);
   }
}
```
Refere to the selected IoC container documentation for more information on its integration with .NET Core 3.
