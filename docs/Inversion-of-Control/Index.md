The [Radical](https://github.com/RadicalFx/radical) Presentation toolkit is completely based on Inversion of Control and Dependency Injection principles but does not force the end user to use any predefined IoC toolkit.

Using a IoC framework is not a requirement at all although some default services implementation relies on the IServiceProvider interface (that exists in the .net framework since v1).

Using a IoC framework is, on the other hand, highly suggested since that the benefit and simplification introduced in the application management greatly overlaps the learning curve of the introduction of the IoC container.

We currently provide out-of-the-box 2 different implementation for 2 different IoC containers:

* Castle Windsor: the [nuget](http://nuget.org/) package [Radical.Windows.Presentation.CastleWindsor](http://nuget.org/packages/Radical.Windows.Presentation.CastleWindsor) gives you, without any effort, all the infrastructure required to build MVVM applications based on Windsor as IoC container;
* Puzzle Container: at the time of this writing there are no IoC containers that supports WinRT (for Windows 8 store apps) so we decided to provide our own IoC container to get, in store apps, the same support that “desktop” IoC containers gives to desktop apps;
* Unity v2 and Unity v3: the nuget packages [Radical.Windows.Presentation.Unity2](http://nuget.org/packages/Radical.Windows.Presentation.Unity2) and [Radical.Windows.Presentation.Unity3](http://nuget.org/packages/Radical.Windows.Presentation.Unity3) gives you, without any effort, all the infrastructure required to build MVVM applications based on Unity as IoC container;
* Autofac: the nuget package [Radical.Windows.Presentation.Autofac](http://nuget.org/packages/Radical.Windows.Presentation.Autofac) gives you, without any effort, all the infrastructure required to build MVVM applications based on Autofac as IoC container;