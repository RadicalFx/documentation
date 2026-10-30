Radical samples
===============

Each folder is a sample: a standalone application, in the `Wpf` folder. All samples are part of the `Samples.Wpf.slnx` solution.

### Getting started

| Sample | Description |
|---|---|
| [basic](basic) | The minimal application: a shell view and its view model, wired by conventions. |
| [splash-screen](splash-screen) | A splash screen with custom content, shown while the startup work runs. |
| [third-party-container](third-party-container) | Booting through the generic host, using Autofac as the container. |

### Application structure

| Sample | Description |
|---|---|
| [modules](modules) | An application composed of modules, with contracts, custom regions and shared services. |
| [plugins](plugins) | Plugins, defined in separate assemblies, adding content to the shell regions. |

### UI composition

| Sample | Description |
|---|---|
| [async-regions](async-regions) | Regions whose content is loaded asynchronously, including partial views from another assembly. |
| [tab-control-region](tab-control-region) | Adding, activating and removing views in a `TabControl` region. |

### Presentation

| Sample | Description |
|---|---|
| [open-child-window](open-child-window) | Opening a child window, or a dialog, from a view model through the message broker. |
| [view-model-as-resource](view-model-as-resource) | Exposing the view model as a static resource of its view. |
| [grayscale-effect](grayscale-effect) | The grayscale shader effect. |

### Commanding

| Sample | Description |
|---|---|
| [auto-command-binding](auto-command-binding) | Binding commands to view model methods by convention. |
| [auto-command-binding-with-templates](auto-command-binding-with-templates) | Auto command binding inside data templates. |
| [auto-command-binding-with-nested-source](auto-command-binding-with-nested-source) | Auto command binding to a nested source. |

### Behaviors

| Sample | Description |
|---|---|
| [busy-status](busy-status) | Showing a busy indicator while the view model works. |
| [cue-banner](cue-banner) | Placeholder text for empty text boxes. |
| [drag-and-drop](drag-and-drop) | Drag and drop handled by view model commands. |
| [focus](focus) | Moving the focus from the view model. |
| [autocomplete](autocomplete) | Integrating a third-party autocomplete control. |

### Model

| Sample | Description |
|---|---|
| [memento-simple-graph](memento-simple-graph) | Change tracking, undo and redo on a simple object graph. |
| [memento-complex-graph](memento-complex-graph) | Change tracking, undo and redo on a graph with collections. |
| [entity-view-primer](entity-view-primer) | Wrapping a collection in an `EntityView`. |
| [entity-view-custom-property-mapping](entity-view-custom-property-mapping) | Adding custom properties to an `EntityView`. |
| [data-annotations-validation](data-annotations-validation) | Validating view models using DataAnnotations. |
