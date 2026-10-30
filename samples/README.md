Radical samples
===============

Each folder is a sample: a standalone WPF application in the `Wpf` folder and, when available, a standalone Avalonia application in the `Avalonia` folder. WPF samples are part of the `Samples.Wpf.slnx` solution, Avalonia samples are part of the `Samples.Avalonia.slnx` solution.

Avalonia samples use the Radical.Avalonia pre-release packages, available on the [Radical pre-releases feed](https://f.feedz.io/radical-framework/pre-releases/nuget/index.json) configured in `nuget.config`. They run on Windows, macOS and Linux.

### Getting started

| Sample | Description | Avalonia |
|---|---|---|
| [basic](basic) | The minimal application: a shell view and its view model, wired by conventions. | Yes |
| [splash-screen](splash-screen) | A splash screen with custom content, shown while the startup work runs. | Yes |
| [third-party-container](third-party-container) | Booting through the generic host, using Autofac as the container. | Yes |

### Application structure

| Sample | Description | Avalonia |
|---|---|---|
| [modules](modules) | An application composed of modules, with contracts, custom regions and shared services. | Yes |
| [plugins](plugins) | Plugins, defined in separate assemblies, adding content to the shell regions. | Yes |

### UI composition

| Sample | Description | Avalonia |
|---|---|---|
| [async-regions](async-regions) | Regions whose content is loaded asynchronously, including partial views from another assembly. | Yes |
| [tab-control-region](tab-control-region) | Adding, activating and removing views in a `TabControl` region. | Yes |

### Presentation

| Sample | Description | Avalonia |
|---|---|---|
| [open-child-window](open-child-window) | Opening a child window, or a dialog, from a view model through the message broker. | Yes |
| [view-model-as-resource](view-model-as-resource) | Exposing the view model as a static resource of its view. | Yes |
| [grayscale-effect](grayscale-effect) | The grayscale shader effect. | No: Avalonia has no custom pixel shader effects. |

### Commanding

| Sample | Description | Avalonia |
|---|---|---|
| [auto-command-binding](auto-command-binding) | Binding commands to view model methods by convention. | Yes |
| [auto-command-binding-with-templates](auto-command-binding-with-templates) | Auto command binding inside data templates. | Yes |
| [auto-command-binding-with-nested-source](auto-command-binding-with-nested-source) | Auto command binding to a nested source. | Yes |

### Behaviors

| Sample | Description | Avalonia |
|---|---|---|
| [busy-status](busy-status) | Showing a busy indicator while the view model works. | Yes |
| [cue-banner](cue-banner) | Placeholder text for empty text boxes. | Yes |
| [drag-and-drop](drag-and-drop) | Drag and drop handled by view model commands. | Yes |
| [focus](focus) | Moving the focus from the view model. | Yes |
| [autocomplete](autocomplete) | Integrating a third-party autocomplete control. | Yes, using the Avalonia `AutoCompleteBox`. |

### Model

| Sample | Description | Avalonia |
|---|---|---|
| [memento-simple-graph](memento-simple-graph) | Change tracking, undo and redo on a simple object graph. | Yes |
| [memento-complex-graph](memento-complex-graph) | Change tracking, undo and redo on a graph with collections. | No: uses `EntityView`, see the EntityView samples. |
| [entity-view-primer](entity-view-primer) | Wrapping a collection in an `EntityView`. | No: Avalonia bindings and DataGrid don't support custom type descriptors. |
| [entity-view-custom-property-mapping](entity-view-custom-property-mapping) | Adding custom properties to an `EntityView`. | No: Avalonia bindings and DataGrid don't support custom type descriptors. |
| [data-annotations-validation](data-annotations-validation) | Validating view models using DataAnnotations. | Yes |
