## Change Tracking Service API

The [memento service](change-tracking-service.md) implement the `IChangeTrackingService` interface, that inherits from the `IRevertibleChangeTracking`, `IDisposable` and `IComponent` interfaces.

All the memento entities implement the `IMemento` interface.

### Basic operations

* `Attach( IMemento item )` / `Detach( IMemento entity )`: attach and detach are the 2 methods to manually control when an instance is attached or detached to and from the memento instance. As soon as an instance is attached it will be tracked for changes and the memento will stop tracking it at detach time.

* `Undo()` /  `Redo()`: Undo and Redo controls the state of the tracked entities, calling `Undo` will revert the last tracked operation, if any, calling `Redo` will apply the last operation that has been undone, if any;
* `CanRedo` and `CanUndo` allows the user code to determine if calling Undo and Redo operations something will be done; 
* `RegisterTransient( Object entity )` and `RegisterTransient( Object entity, Boolean autoRemove )` allows the user to register an entity as a transient, versus persistent, entity.  
    Registering transient entities is not really required for the memento to work properly, it is on the other hand very handy for the user if the code needs at a certain point to deal with a storage trying to understand which operations should be done to align the storage with the current in memory state. if `autoRemove` is set to `true` (*the default value*) and `RejectChanges()`, or an `Undo()` that removes the last `IChange` of the object, is called the object then is automatically removed from the list of the new objects.  
    When it is the case to set `autoRemove` to `false`? The question should be: a transient untouched entity should be considered as changed? Or from the user perspective: a transient untouched entity should trigger a question to the user such as "Do you want to save your changes?", if the answer is yes then set `autoRemove` to `false`;
* `UnregisterTransient( Object entity )` manually remove the given transient entity from the list of transient entities;
* `HasTransientEntities` determines if the the memento is currently tracking transient entities;
* `GetEntityState( Object entity )` return, given a tracked entity, the current entity state as seen by the memento, the returned value is a `EntityTrackingStates` enumeration that can assume one, or more, of the following values:
    *  `None`:  The state of the entity is not changed, the entity is not transient or the entity is not tracked;
    *  `IsTransient`: The entity is registered as transient;
    *  `AutoRemove`: if an entity is marked as `AutoRemove` (the default behavior) and `RejectChanges`, or an `Undo` that removes the last `IChange` of the entity, is called then the entity is  automatically removed from the list of the transient entities;
    *  `HasBackwardChanges`: The entity is changed and has changes that can be undone, meaning that `Undo` can n be called;
    *  `HasForwardChanges`: The entity has changes that can be reapplied, meaning that `Redo` can be called;
* `BeginAtomicOperation()`: begins an [atomic operation](atomic-operations.md) returned as a `IAtomicOperation` instance on which the caller is expected to call `Complete()` to store it in the changes stack;

### Searches

* `GetEntities()` and `GetEntities( EntityTrackingStates sateFilter, Boolean exactMatch )` allows the caller to retrieve the list of the currently tracked entities and/or to search for them based on their current state;

### Suspend and Resume

* Using `Suspend()` and `Resume()` it is possible to momentarily ask the memento service to stop tracking changes to the currently tracked entities;
* `IsSuspended` determines if the memento is currently suspended or not;

### Events

* `TrackingServiceStateChanged`: each time the internal state of the memento changes the `TrackingServiceStateChanged` event is raised;
* `ChangesAccepted` and `ChangesRejected` are called, respectively, when changes are accepted or rejected;
* `AcceptingChanges` and `RejectingChanges` are events raised to inform that the memento is in the process of accepting or rejecting changes, both can be cancelled;

### Bookmarks

* `CreateBookmark()`: allows to create a bookmark, an `IBookmark` instance, that represents a point in the changes stack useful to revert changes to a known point in a single step;
* `Revert( IBookmark bookmark )`: given a bookmark revert all the changes at the given point in time deleting the bookmark once done;
* `Validate( IBookmark bookmark )`: verifies that a bookmark is still valid, it exists in the changes stack;

### ChangeSets and Advisories

* `GetChangeSet()` and `GetChangeSet( IChangeSetFilter builder )` return a `IChangeSet` that is a list of all the currently tracked changes, it is possible to use a `IChangeSetFilter` implementation to filter the list of changes returned;
* `GetAdvisory()` and `GetAdvisory( IAdvisoryBuilder builder )` return a `IAdvisory`, an advisory is a list of proposed actions that the memento thinks should be applied to align the current state in memory with a persistent storage. If a tracked entity is registered as transient and has changes the advisory will suggest to `create` it, on the other hand if it is not registered as transient and has pending changes will suggest to `update` it. One interesting feature is that if a tracked entity is removed from a `MementoEntityCollection<T>` the advisory will suggest to `delete` it.  
  *Note: It is up to the user to apply the suggested changes to the persistent storage*.