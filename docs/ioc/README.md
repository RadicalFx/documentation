## Inversion of Control

[Radical](https://github.com/RadicalFx/radical) Presentation toolkit depends on Inversion of Control and Dependency Injection principles but does not force the end user to use any predefined IoC toolkit.

By default Radical uses the Microsoft dependency injection seam, exposed to users by the `IServiceCollection` and `IServiceProvider` interfaces.

Support for [third party containers](third-party.md) is provided throught generic host support, all containers supported by the Microsoft extensions infrastructure can be used with Radical. 

### Registering custom dependencies

To register custom dependencies into the IoC conatiner a dependency installer is required:

- Create a class that implements the `IDependenciesInstaller` interface. The class can be created in any assembly that is deployed in the application bin folder, the assembly scanning process will find it during the application startup pahse
- At startup the class `Install` method will be invoked and custom registrations can be performed against the provided `IServiceCollection` instance.

The following is a custom installer sample class:

```csharp
class DefaultInstaller : IDependenciesInstaller
{
   public void Install(BootstrapConventions conventions, IServiceCollection services, IEnumerable<Type> assemblyScanningResults)
   {
      services.AddSingleton<MyType>();
   }
}
```
