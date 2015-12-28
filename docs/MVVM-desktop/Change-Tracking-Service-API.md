The memento service implement the `IChangeTrackingService` interface, that inherits from the `IRevertibleChangeTracking`, `IDisposable` and `IComponent` interfaces.
* `CanRedo` and `CanUndo` allows the user code to determine if calling Undo and Redo operations something will be done; 
* `RegisterTransient( Object entity )` and `RegisterTransient( Object entity, Boolean autoRemove )` allows the user to register an entity as a transient, versus persistent, entity.  
Registering transient entities is not really required for the memento to work properly, it is on the other hand very handy for the user if the code needs at a certain point to deal with a storage trying to understand which operations should be done to align the storage with the current in memory state. if `autoRemove` is set to `true` (*the default value*) and `RejectChanges()`, or an `Undo()` that removes the last `IChange` of the object, is called the object then is automatically removed from the list of the new objects.  
When it is the case to set `autoRemove` to `false`? The question should be: a transient untouched entity should be considered as changed? Or from the user perspective: a transient untouched entity should trigger a question to the user such as "Do you want to save your changes?", if the answer is yes then set `autoRemove` to `false`;
    *  `None`:  The state of the entity is not changed, the entity is not transient or the entity is not tracked;
    *  `IsTransient`: The entity is registered as transient;
    *  `AutoRemove`: if an entity is marked as `AutoRemove` (the default behavior) and `RejectChanges`, or an `Undo` that removes the last `IChange` of the entity, is called then the entity is  automatically removed from the list of the transient entities;
    *  `HasBackwardChanges`: The entity is changed and has changes that can be undone, meaning that `Undo` can n be called;
    *  `HasForwardChanges`: The entity has changes that can be reapplied, meaning that `Redo` can be called;
* `BeginAtomicOperation()`: begins an [[atomic operation|Atomic-operations]] returned as a `IAtomicOperation` instance on which the caller is expected to call `Complete()` to store it in the changes stack;

### Events

* `TrackingServiceStateChanged`: each time the internal state of the memento changes the `TrackingServiceStateChanged` event is raised;

### Bookmarks

### ChangeSets and Advisories
*Note: It is up to the user to apply the suggested changes to the persistent storage*.