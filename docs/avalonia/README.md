## Radical for Avalonia

[Radical.Avalonia](https://github.com/RadicalFx/Radical.Avalonia) is the [Avalonia](https://avaloniaui.net/) counterpart of Radical.Windows. It mirrors the Radical.Windows API, using the `Radical.Avalonia` root namespace instead of `Radical.Windows`, so the concepts described in this documentation apply to both. Where Avalonia works differently from WPF, the differences are listed in [Migrating from Radical.Windows](migrating-from-radical-windows.md).

Radical.Avalonia targets Avalonia 12 and runs on every platform Avalonia supports: desktop (Windows, macOS, Linux), mobile (Android, iOS) and browser.

### Steps to bootstrap your project in 3 minutes

* Create a new Avalonia application, e.g. using `dotnet new avalonia.app`;
* Add, using NuGet, a reference to [Radical.Avalonia](https://www.nuget.org/packages/Radical.Avalonia);
* Delete the default `MainWindow.axaml`;
* Add a `Presentation` folder to the project;
  * `Presentation` is the default location, based on conventions, where Radical looks for Views and ViewModels;
* Create 2 new items in the `Presentation` folder:
  * An Avalonia window named Main**View**.axaml \(\*View is important for the default conventions\);
  * A class Main**ViewModel** \(&lt;_ViewName_&gt;ViewModel is important for the default conventions\);
* In `App.axaml.cs`, replace the main window creation with a single line of code:

```csharp
public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        this.AddRadicalApplication<Presentation.MainView>();

        base.OnFrameworkInitializationCompleted();
    }
}
```

**Press F5 and you are up & running**: the `MainView` window will be shown. The following things happen:

* The application boots when the desktop lifetime raises its `Startup` event
* All the default and required services \(for MVVM and UI Composition\) are wired into the IoC container (using `IServiceCollection`)
* The `MainView` is designed as the main window
* At boot time the `MainView` is resolved and using the conventions engine the `MainViewModel` is setup and set as the `DataContext` of the `MainView`
* Finally the `MainView` is shown.

`AddRadicalApplication` is an extension method defined in the `Avalonia` namespace, as the WPF one is defined in `System.Windows`.

#### Compiled bindings

Avalonia compiles bindings by default, which requires the type of the data context to be known at compile time. The view model is set as the view `DataContext` at runtime by the conventions engine, so declare its type in the view:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:vm="using:MyApp.Presentation"
        x:Class="MyApp.Presentation.MainView"
        x:DataType="vm:MainViewModel">
    <TextBlock Text="{Binding Title}" />
</Window>
```

#### Radical types in XAML

Radical types are referenced in XAML by importing their CLR namespaces:

```xml
<Window xmlns:rg="using:Radical.Avalonia.Regions"
        xmlns:mk="using:Radical.Avalonia.Markup"
        xmlns:rb="using:Radical.Avalonia.Behaviors"
        xmlns:rc="using:Radical.Avalonia.Converters">
    <ContentPresenter rg:RegionService.Region="{rg:ContentPresenterRegion Name=MainContent}" />
</Window>
```

### Desktop, mobile and browser

On desktop the shell is shown in a window. On single-view platforms (mobile and browser) there are no windows: the shell is the application main view.

The shell can be any control:

* A `Window` shell can only be used on desktop. On single-view platforms booting fails with a `NotSupportedException`.
* Any other control, e.g. a `UserControl`, can be used on every platform. On desktop Radical hosts it in a new window; on single-view platforms it becomes the main view.

To share the same shell across platforms, create `MainView` as a `UserControl`:

```csharp
public override void OnFrameworkInitializationCompleted()
{
    // MainView is a UserControl
    this.AddRadicalApplication<Presentation.MainView>();

    base.OnFrameworkInitializationCompleted();
}
```

The same applies to the [splash screen](../how-tos/splash-screen.md): the default splash screen view is a `UserControl`. On desktop it's hosted in a window configured using the `SplashScreenConfiguration` window settings; on single-view platforms it's the main view until the shell replaces it, and the window settings are ignored.

### Generic Host

As with Radical.Windows, the application bootstrapping process can be delegated to the [generic host](../ioc/third-party.md). With the desktop lifetime, start the host when the application starts and stop it when the application exits:

```csharp
public override void OnFrameworkInitializationCompleted()
{
    var host = new HostBuilder()
        .AddRadicalApplication<Presentation.MainView>()
        .Build();

    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
        desktop.Startup += async (s, e) =>
        {
            await host.StartAsync();
        };

        desktop.Exit += async (s, e) =>
        {
            using (host)
            {
                await host.StopAsync();
            }
        };
    }

    base.OnFrameworkInitializationCompleted();
}
```

### What’s next

The best topic to read now is basic [concepts about the ViewModel](../mvvm/abstract-view-model.md). Code samples in this documentation use WPF; [Migrating from Radical.Windows](migrating-from-radical-windows.md) lists what changes with Avalonia.
