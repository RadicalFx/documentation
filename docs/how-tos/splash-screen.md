## Splash screen

`Radical` utilizes its own internal [UI Composition](../ui-composition/index.md) engine to add support for splash screens at application startup:

```csharp
class App : Application
{
   public App()
   {
      this.AddRadicalApplication<MainView>(configuration => 
      {
         configuration.EnableSplashScreen();
      });
   }
}
```

Enabling splash screen support is as easy as calling the `EnableSplashScreen` method on the application configuration.

#### Splash screen content

Splash screen content is managed using the `UI Composition` engine. To add content to the splash screen is enough to define a [partial view](../ui-composition/index.md#automatic-aka-partial-regions) named `SplashScreenContent`:

```
.Presentation
  .Partial
     .SplashScreenContent
```

The `View`, along with its `ViewModel` if any, defined in the `*.Presentation.Partial.SplashScreenContent` namespace will be used to populate the splash screen.

#### Splash screen configuration

It is possible to use the `SplashScreenConfiguration` class to define some splash screen settings:

* `SizeToContent`: Determines the way the splash screen hosting window is dimensioned, the default value is `WidthAndHeight`;
* `WindowStartupLocation`: The splash screen startup location, the default value is `CenterScreen`;
* `WindowStyle`: The splash screen window style, the default value is `None`.
* `StartupAsyncWork`: Defines the work that should be executed asynchronously while the splash screen is running;
* `Height`: Defines the Height of the splash screen window if the `SizeToContent` value is `Manual` or `Width`; otherwise is ignored;
* `Width`: Defines the Width of the splash screen window if the `SizeToContent` value is `Manual` or `Height`; otherwise is ignored;
* `MinWidth`: The Minimum Width of the splash screen window. The default value is `585`;
* `MinHeight`: The Minimum Height of the splash screen window. The default value is `335`;
* `MinimumDelay`: Represents the minimum time, in milliseconds, the splash screen will be shown; 
* `SplashScreenViewType`: Defines the default view that `Radical` uses to host the splash screen content;
