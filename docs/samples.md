## Samples

The [documentation repository](https://github.com/RadicalFx/documentation/tree/master/samples) contains a set of samples, each one a standalone application focused on a single feature. Each sample is a folder containing the WPF application in the `Wpf` folder. All samples are part of the `samples/Samples.Wpf.slnx` solution.

### Getting started

| Sample | Description | Documentation |
|---|---|---|
| [basic](https://github.com/RadicalFx/documentation/tree/master/samples/basic) | The minimal application: a shell view and its view model, wired by conventions. | [Quick start](README.md) |
| [splash-screen](https://github.com/RadicalFx/documentation/tree/master/samples/splash-screen) | A splash screen with custom content, shown while the startup work runs. | [Create a splash screen](how-tos/splash-screen.md) |
| [third-party-container](https://github.com/RadicalFx/documentation/tree/master/samples/third-party-container) | Booting through the generic host, using Autofac as the container. | [Third party DI containers](ioc/third-party.md) |

### Application structure

| Sample | Description | Documentation |
|---|---|---|
| [modules](https://github.com/RadicalFx/documentation/tree/master/samples/modules) | An application composed of modules, with contracts, custom regions and shared services. | [Create a custom region](ui-composition/create-a-custom-region.md) |
| [plugins](https://github.com/RadicalFx/documentation/tree/master/samples/plugins) | Plugins, defined in separate assemblies, adding content to the shell regions. | [UI Composition](ui-composition/index.md) |

### UI composition

| Sample | Description | Documentation |
|---|---|---|
| [async-regions](https://github.com/RadicalFx/documentation/tree/master/samples/async-regions) | Regions whose content is loaded asynchronously, including partial views from another assembly. | [UI Composition](ui-composition/index.md) |
| [tab-control-region](https://github.com/RadicalFx/documentation/tree/master/samples/tab-control-region) | Adding, activating and removing views in a `TabControl` region. | [TabControl region](ui-composition/tabcontrol-region.md) |

### Presentation

| Sample | Description | Documentation |
|---|---|---|
| [open-child-window](https://github.com/RadicalFx/documentation/tree/master/samples/open-child-window) | Opening a child window, or a dialog, from a view model through the message broker. | [Message broker](messaging/message-broker.md) |
| [view-model-as-resource](https://github.com/RadicalFx/documentation/tree/master/samples/view-model-as-resource) | Exposing the view model as a static resource of its view. | [ViewModels as resources](mvvm/viewmodels-as-resources.md) |
| [grayscale-effect](https://github.com/RadicalFx/documentation/tree/master/samples/grayscale-effect) | The grayscale shader effect. | |

### Commanding

| Sample | Description | Documentation |
|---|---|---|
| [auto-command-binding](https://github.com/RadicalFx/documentation/tree/master/samples/auto-command-binding) | Binding commands to view model methods by convention. | [Auto Command binding](markup/auto-command-binding.md) |
| [auto-command-binding-with-templates](https://github.com/RadicalFx/documentation/tree/master/samples/auto-command-binding-with-templates) | Auto command binding inside data templates. | [Auto Command binding](markup/auto-command-binding.md) |
| [auto-command-binding-with-nested-source](https://github.com/RadicalFx/documentation/tree/master/samples/auto-command-binding-with-nested-source) | Auto command binding to a nested source. | [Auto Command binding](markup/auto-command-binding.md) |

### Behaviors

| Sample | Description | Documentation |
|---|---|---|
| [busy-status](https://github.com/RadicalFx/documentation/tree/master/samples/busy-status) | Showing a busy indicator while the view model works. | [Busy status manager](behaviors/busy-status-manager.md) |
| [cue-banner](https://github.com/RadicalFx/documentation/tree/master/samples/cue-banner) | Placeholder text for empty text boxes. | |
| [drag-and-drop](https://github.com/RadicalFx/documentation/tree/master/samples/drag-and-drop) | Drag and drop handled by view model commands. | |
| [focus](https://github.com/RadicalFx/documentation/tree/master/samples/focus) | Moving the focus from the view model. | [Manage focus](how-tos/manage-focus.md) |
| [autocomplete](https://github.com/RadicalFx/documentation/tree/master/samples/autocomplete) | Integrating a third-party autocomplete control. | [Upgrade guide](upgrade-guides/1.x-to-2.0.0.md) |

### Model

| Sample | Description | Documentation |
|---|---|---|
| [memento-simple-graph](https://github.com/RadicalFx/documentation/tree/master/samples/memento-simple-graph) | Change tracking, undo and redo on a simple object graph. | [Simple ViewModel graphs](mvvm/memento-change-tracking-simple-view-model.md) |
| [memento-complex-graph](https://github.com/RadicalFx/documentation/tree/master/samples/memento-complex-graph) | Change tracking, undo and redo on a graph with collections. | [Collections and complex ViewModel graphs](mvvm/memento-change-tracking-collection-and-complex-view-model.md.md) |
| [entity-view-primer](https://github.com/RadicalFx/documentation/tree/master/samples/entity-view-primer) | Wrapping a collection in an `EntityView`. | |
| [entity-view-custom-property-mapping](https://github.com/RadicalFx/documentation/tree/master/samples/entity-view-custom-property-mapping) | Adding custom properties to an `EntityView`. | |
| [data-annotations-validation](https://github.com/RadicalFx/documentation/tree/master/samples/data-annotations-validation) | Validating view models using DataAnnotations. | [Validation and Validation Services](mvvm/validation-validationservice.md) |
