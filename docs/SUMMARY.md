# Summary

* [Home](README.md)

## Presentation

* [AbstractViewModel](/mvvm/abstract-view-model.md)
* [Conventions](/mvvm/conventions.md)
  * [Bootstrap Conventions](/mvvm/bootstrap-conventions.md)
  * [Runtime Conventions](/mvvm/runtime-conventions.md)
  * [Conventions override](/mvvm/conventions-override.md)
* [Commands and DelegateCommand](/mvvm/delegate-command.md)
* [IViewResolver](/mvvm/iview-resolver.md)
  * [Default view behaviors](/mvvm/default-view-behaviors.md)
  * Intercepting [view life cycle events](/mvvm/view-life-cycle-events.md):
    * In ViewModels: [Callback expectations](/mvvm/callback-expectations.md)
    * Using broker messages: [notify messages](/mvvm/notify-messages.md)
* [Message broker MVVM built-in messages](/mvvm/built-in-messages.md)
* [Application boot process demystified](/mvvm/boot-process-demystified.md)
  * [Application shutdown](/mvvm/application-shutdown.md)
  * [Singleton applications](/mvvm/singleton-applications.md)
* [AbstractMementoViewModel](/mvvm/abstract-memento-view-model.md)
  * [Simple ViewModel graphs](/mvvm/memento-change-tracking-simple-view-model.md)
  * [Collections and complex ViewModel graphs](/mvvm/memento-change-tracking-collection-and-complex-view-model.md.md)
* [Validation and Validation Services](/mvvm/validation-validationservice.md)

## UI Composition

* [UI Composition](/ui-composition/index.md)
  * [Region content lifecycle](/ui-composition/region-content-lifecycle.md)
  * [TabControl region](/ui-composition/tabcontrol-region.md)
  * [Create a custom region](/ui-composition/create-a-custom-region.md)

## Concepts

* [Inversion of Control](/ioc/index.md)
  * [Castle Windsor](/ioc/windsor.md)
  * [Autofac](/ioc/autofac.md)
  * [Unity \(v2 & v3\)](/ioc/unity.md)
  * [Puzzle Container](/ioc/puzzle.md)
* Entities
  * [Property System](/entities/property-system.md)
* [Messaging and Message Broker](/messaging/message-broker.md)
  * [POCO messages](/messaging/poco-messages.md)
  * [Standalone message handlers](/messaging/abstract-message-handler.md)
* [Observers](/observers/index.md)
  * [PropertyObserver](observers/property-observer.md)
  * [MementoObserver](/observers/memento-observer.md)
  * [BrokerObserver](/observers/broker-observer.md)

## Memento

* [Change Tracking Service](/memento/change-tracking-service.md)
  * [MementoEntity and MementoEntityCollection](/memento/memento-entities.md)
  * Handling change tracking:
    * [Simple model](/memento/simple-model.md)
    * [Collections](/memento/collections.md)
    * [Complex objects graph](/memento/complex-graph.md)
  * [Atomic operations](/memento/atomic-operations.md)
  * [Change Tracking Service API](/memento/change-tracking-service-api.md)
  * [Property Metadata for the ChangeTrackingService](/memento/memento-metadata.md)
  * [Handling collection sync](/memento/handling-collection-sync.md)
* States
  * [Property State](/memento/property-state.md)


## Behaviors

* [DataGrid Behaviors](/behaviors/datagrid-behaviors.md)
* [Password](/behaviors/password.md)
* [Generic routed event handler to command behavior](/behaviors/routed-event-to-command.md)
* [Overlay adorner](/behaviors/overlay-adorner.md)
  * [Busy status manager](/behaviors/busy-status-manager.md)
* TextBox behaviors:
  * [Command](/behaviors/textbox-command.md)
  * [Auto select](/behaviors/textbox-auto-select.md)
  * [DisableUndoManager](/behaviors/textbox-disable-undo-manager.md) (.Net 3.5 only)

## Markup Extensions

- [Editor binding](/markup/editor-binding.md)
- [Auto Command binding](/markup/auto-command-binding.md)

## How to

- [Get the view of a given view model](/how-tos/get-view-of-view-model.md)
- [Bi-directional communication between different windows/views](/how-tos/windows-bi-directional-communication.md)
- [Handle the busy status during async/long running operations](/how-tos/async-long-running-busy-status.md)
- [Implement a customer improvement program](/how-tos/customer-improvement-program.md)
- [Manage focus](/how-tos/manage-focus.md)
- [Create a splash screen](/how-tos/splash-screen.md)
- [Access view model after view is closed](/how-tos/access-view-model-after-view-closed.md)
- [Intercept ViewModels before it's used](/how-tos/intercept-viewmodels-before-usage.md)
