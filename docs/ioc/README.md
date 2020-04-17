## Inversion of Control

[Radical](https://github.com/RadicalFx/radical) Presentation toolkit depends on Inversion of Control and Dependency Injection principles but does not force the end user to use any predefined IoC toolkit.

By default Radical uses the Microsoft dependency injection seam, exposed to users by the `IServiceCollection` and `IServiceProvider` interfaces.

Support for [third party containers](third-party.md) is provided throught generic host support, all containers supported by the Microsoft extensions infrastructure can be used with Radical. 
