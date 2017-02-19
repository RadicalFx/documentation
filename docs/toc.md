### Introduction

  * [[Building the source code]];
  * [[Included sample applications]];
  * [[Release management process]];
  * [[A note on versioning]];
  * [[Contribution guideline]];

### How to

  * [[How to: get the view of a given view model|How to get the view of a given view model]];
  * [[How to: bi-directional communication between different windows/views|How to bi-directional communication between different windows views]];
  * [[How to: handle the busy status during async/long running operations|How to handle the busy status during async long running operations]];

### MVVM and UI Composition

  * [[Quick Start (WPF)]];
  * [[Quick Start (Universal App)]];

### Radical Presentation: MVVM and UI Composition toolkit

*([nuget][1] package: [Radical.Windows.Presentation][4])*

  * [[Inversion of Control]]:
    * [[Castle Windsor]];
    * [[Autofac]];
    * [[Unity]] (v2 &amp; v3);
    * [[Puzzle Container]] (for WinRT & WP apps);
  * [[Conventions]]:
    * [[Bootstrap conventions]];
    * [[Runtime conventions]];
    * [[Conventions override]];
  * MVVM;
    * [[AbstractViewModel]];
    * [[Validation and Validation Services]];
    * [[The boot process demystified]];
        * [[Application shutdown]];
        * [[Singleton applications]];
        * [[WinRT peculiarities]];
    * [[The “IViewResolver”|The IViewResolver]]: how to get view instances at runtime;
        * [[Default view behaviors]];
        * Intercepting [[view life cycle events]]:
            * In the view model: [[Callback expectations]];
            * Using broker messages: [[notify messages]];
        * [[Intercepting the view model before it is used]];
        * [[Accessing the view model after the view is closed]];
    * [[The message broker]];
        * [[Built-in messages]];
        * [[Stand alone message handlers]];
    * [[AbstractMementoViewModel and the Change Tracking Service]];
        * [[Handling change tracking in a simple ViewModel]];
        * [[Handling change tracking in collections and complex ViewModels graph]];
    * [[Focus management]];
    * [WinRT] Charms handling;
        * [WinRT] Settings Charm Conventions plugin;
    * [[UI Composition]];
        * [[Region content lifecycle]];
        * [[The TabControl region]];
        * [[Create a custom region]];
    * [[Splash screen management]];

### Radical

*([nuget][1] package: [Radical][2])*

  * [[The message broker]];
    * [[POCO messages]];
  * [[Entity and EntityCollection]];
  * [[Property System]];
  * IBindingListView implementation: [[EntityView]];
  * [[Change Tracking Service]];
    * [[MementoEntity and MementoEntityCollection]];
    * [[Handling change tracking in a simple model]];
    * [[Handling change tracking in collections]];
    * [[Handling change tracking in complex objects graph]];
    * [[Atomic operations]];
    * [[Change Tracking Service API]];
    * [[Property Metadata for the Change Tracking Service]];
    * [[Handling collection sync]]
  * System.Enum metadata and localization;
  * [[Observers concepts]];
    * [[PropertyObserver]];
    * [[MementoObserver]];
    * [[BrokerObserver]];
    * [[EntityViewListChangedObserver]];
  * [[Customer Improvement Program]];

### Radical Windows

*([nuget][1] package: [Radical.Windows][3])*

  * Collections:
    * ObservableEntityCollection;
    * EntityCollectionView;
  * Behaviors:
    * Auto Complete;&nbsp;
    * Cue banner;
    * Drag &amp; Drop;
    * Empty place holder;
    * List view behaviors:
      * Column manager;
      * Column header commanding;&nbsp;&nbsp;
      * Auto size, selected **items** binding, sort and item double click management;
    * [[Overlay adorner]];
      * [[Busy status manager]];
    * Text box:
      * [[Command]];
      * [[Auto select]];
      * [[DisableUndoManager]] (.Net 3.5 only);
    * [[Password]];
    * Window control box;
    * [[Generic routed event handler to command behavior]];
  * Markup:&nbsp;
    * [[Editor binding]];
    * Command bindings:
      * Auto command binding;
      * Behavior auto command binding;
  * Effects:
    * Gray scale
  * Converters:
    * Singleton converters (concepts);
    * BooleanBusyStatusConverter;
    * BooleanToVisibilityConverter;
    * NotConverter;
    * EnumCaptionConverter and EnumDescriptionConverter;
    * NullToVisibilityConverter;
  * Delegate commands;
  * Resizer control;
  * Observers:
    * NotifyCollectionChangedMonitor;

### Radical Design

  * Design time data: concepts;
  * how to;
  
[1]: http://nuget.org
[2]: http://nuget.org/packages/Radical
[3]: http://nuget.org/packages/Radical.Windows
[4]: http://nuget.org/packages/Radical.Windows.Presentation