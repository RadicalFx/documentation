## Migrating from Radical.Windows to Radical.Avalonia

Radical.Avalonia mirrors the Radical.Windows API, so moving an application from WPF to Avalonia is mostly a matter of changing `using` directives and XAML namespaces. This page lists where the API or the behavior differs, because Avalonia works differently from WPF, and what is not available.

### Namespaces and types

All namespaces move from `Radical.Windows` to `Radical.Avalonia`, e.g. `Radical.Windows.Presentation` becomes `Radical.Avalonia.Presentation`. The `AddRadicalApplication` extension methods move from the `System.Windows` namespace to the `Avalonia` namespace.

In XAML, Radical types are referenced by importing their CLR namespaces, e.g. `xmlns:rg="using:Radical.Avalonia.Regions"`.

WPF types in the public API are replaced by their Avalonia counterparts:

| Radical.Windows | Radical.Avalonia |
|---|---|
| `DependencyObject` | `AvaloniaObject` |
| `FrameworkElement`, `UIElement` | `Control` |
| `Window` | `Window` |
| `Visibility` | `bool` (the `IsVisible` property) |
| `ModifierKeys` | `KeyModifiers` |
| `InputGesture`, `InputBindingCollection` | `KeyGesture`, `IList<KeyBinding>` |
| `Brush` | `IBrush` |
| `IDataObject` | `IDataTransfer` |

### Application boot and shutdown

* **Where to boot.** Call `AddRadicalApplication` in the `App.OnFrameworkInitializationCompleted` override instead of the `App` constructor. With the desktop lifetime, the boot process runs when the lifetime raises the `Startup` event; with no controlled lifetime, e.g. on mobile and browser, it runs immediately. See [Radical for Avalonia](README.md).
* **The shell can be any control.** `UseAsShell<T>` and `AddRadicalApplication<T>` require `T` to be a `Control`, not a `Window`. On desktop a shell that isn't a window is hosted in a new window; on mobile and browser the shell is the main view and cannot be a window.
* **Splash screen.** `SplashScreenConfiguration.WindowStyle` becomes `WindowDecorations`, `None` by default. The default splash screen view is a `UserControl`, hosted in a window on desktop.
* **Window loaded notifications.** Avalonia raises `Loaded` on a window when it's created, not when it's shown. `ViewModelLoaded` messages and `IExpectViewLoadedCallback` callbacks for windows are raised when the window is opened, before the shown notifications.
* **Session ending.** `ApplicationShutdownReason.SessionEnding` is never raised: Avalonia doesn't expose a session ending notification.
* **Cultures.** The cultures feature sets the thread cultures and applies the `FlowDirection` to windows when they're opened.

### Regions

Regions are markup extensions, as in Radical.Windows:

```xml
<Window xmlns:rg="using:Radical.Avalonia.Regions">
    <ContentPresenter rg:RegionService.Region="{rg:ContentPresenterRegion Name=MainContent}" />
    <StackPanel rg:RegionService.Region="{rg:PanelRegion Name=Toolbar}" />
    <TabControl rg:RegionService.Region="{rg:TabControlRegion Name=Documents}" />
</Window>
```

### Commands

| Radical.Windows | Radical.Avalonia |
|---|---|
| `InputBindingCollection InputBindings` | `IList<KeyBinding> InputBindings` |
| `AddGesture(InputGesture)` | `AddGesture(KeyGesture)` |
| `ModifierKeys` | `KeyModifiers` |
| `GetGestures` supports `RoutedCommand` | `GetGestures` supports `DelegateCommand` only: Avalonia has no routed commands |
| `PropertyPath path` on command builders | `string path` |

### Markup extensions

`AutoCommandBinding`, `CommandBinding` and `EditorBinding` work as in Radical.Windows, on top of the Avalonia binding system:

* The binding properties not supported by Avalonia are not available: `ValidatesOn*`, `NotifyOn*`, `ValidationRules`, `XPath`, `IsAsync`, `AsyncState`.
* `CommandBinding` adds the command key bindings to the root element `KeyBindings` once the target is loaded.
* `EditorBinding` sets `UpdateSourceTrigger=PropertyChanged`. Validation errors are reported for properties that enable data validation, such as `TextBox.Text`, with no extra binding settings.
* `BehaviorAutoCommandBinding` and `BehaviorCommandBinding` return a placeholder command, replaced once the behavior attached object is loaded.
* `TypeExtension` is not available.

### Converters

* **Visibility converters produce `bool`**, to be used with the `IsVisible` property. `BooleanToVisibilityConverter` (`TrueValue`, `FalseValue`) and `NullToVisibilityConverter` (`NullValue`) keep their names and parameters, typed as `bool`.
* `BinaryImageConverter` produces an Avalonia `Bitmap`.
* `EnumBooleanConverter` returns `BindingOperations.DoNothing` instead of `Binding.DoNothing`.
* `TimeSpanToKeyTimeConverter` is not available: Avalonia `KeyFrame.KeyTime` already is a `TimeSpan`.

### Behaviors

Behaviors are based on [Xaml.Behaviors for Avalonia](https://github.com/wieslawsoltes/Xaml.Behaviors), the Avalonia counterpart of `Microsoft.Xaml.Behaviors.Wpf`.

* **`Handle`** (routed event to command): set the event using `x:Static`, e.g. `RoutedEvent="{x:Static Button.ClickEvent}"`. `$originalSource` is the same as `$source`: Avalonia routed events have no `OriginalSource`.
* **`TextBoxManager` and `TextBoxCommandBehavior`**: `TextBoxCommandArgs.Modifiers` is a `KeyModifiers`. `TextBoxCommandBehavior` doesn't implement `ICommandSource`.
* **`MouseManager`**: button presses, double clicks and context menu requests are mapped to the Avalonia `PointerPressed`, `DoubleTapped` and `ContextRequested` events.
* **`ScrollViewerManager`**: offsets set before the scroll viewer is loaded, e.g. by a binding, are applied once it's loaded.
* **`BusyStatusManager`, `CueBannerService`, `EmptyPlaceHolderService`, `Overlay` and `OverlayBehavior`** react to changes at any time, not only after the element is loaded, and their adorners can coexist on the same element.
  * `CueBannerService` attaches to `TextBox`. Avalonia `TextBox.PlaceholderText` is an alternative for new code.
  * `OverlayBehavior.Background` is an `IBrush`.

### Drag and drop

* **Dragged data.** Dragged objects travel as in-process `IDataTransfer` items. Instead of `args.Data.GetData(typeof(Person))` or `args.Data.GetData("people")`, use `args.GetData(typeof(Person))` or `args.GetData("people")` on the drag and drop command arguments. `DragDropManager.GetDataFormat` returns the data formats for direct `IDataTransfer` access.
* **`KeyStates`** is a `KeyModifiers`.
* **`AllowedEffects`** are the effects set by the drag source.
* Drag sources, drop targets and drop commands are searched starting from the element under the pointer itself, then its ancestors.

### Not available

The following Radical.Windows features are not available in Radical.Avalonia:

* `GrayscaleEffect`: Avalonia has no custom pixel shader effects.
* `BusyStatusManager.EnableMultiThreadingHost`, `BackgroundVisualHost` and the multithreading busy adorner: Avalonia has a single UI thread.
* `Password`, `PasswordBoxBehavior` and `CueBannerService.PasswordCueBanner`: Avalonia has no `PasswordBox`, a `TextBox` with `PasswordChar` has a bindable `Text`.
* `ListViewManager`, `GridViewColumnManager`, `ListViewColumnHeaderCommandBehavior` and `ScrollIntoViewForListView`: Avalonia has no `ListView`; `ScrollIntoViewForListBox` is available.
* `DataGridBehavior` and `ScrollIntoViewForDataGrid`: the Avalonia `DataGrid` is a separate package, see [RadicalFx/Radical.Avalonia#19](https://github.com/RadicalFx/Radical.Avalonia/issues/19).
* `WindowControlBoxBehavior`: Avalonia windows expose `CanResize`, `WindowDecorations` and `ExtendClientAreaToDecorationsHint` on every platform.
* `Resizer`, `ManipulationBoundaryFeedback` and `BindingErrorTraceListener`: WPF specific; Avalonia logging reports binding errors.
* `IEntityCollectionView` and `ObservableEntityCollection`: Avalonia has no `ICollectionView`.
* `ApplicationBootstrapper` and `Wait`: already obsolete in Radical.Windows.
