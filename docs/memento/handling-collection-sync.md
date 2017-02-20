## Handling collection sync

We have seen how to handle change tracking in MVVM based editor loading the editor given a graph of objects coming from a persistent storage.

What we still miss is the ability to correlate changes back to the persistent graph. Radical does not offer any out-of-the-box automatic support to achieve this requirement but if we think about it the only thing not so easy to deal with are collections.

For class model instances, such as a `Person` and its `PersonViewModel` editor it is straightforward at save time simply copy back all the properties from the `ViewModel` to the model.

On the other hand for collection of objects, where the collection is tracked by the `ChangeTrackingService` we need to understand what has happened to the collection structure in order to:

* Items added to the edited collection need to be created in the underlying data store or simply added to the persistent model;
* Items removed from the edited collection should be deleted from the underlying data store or simply removed from the persistent model;
* Items changed in the edited collection needs to be simply synched back to their corresponding counterpart in the persistent model;

We can leverage the power of change tracking `IAdvisory` to understand what has happened during the editing phase:

```csharp
var advisory = service.GetAdvisory();
var items = advisory.Where( a =>
{
    return a.Target.GetType().Is<Address>() 
       && a.Action == ProposedActions.Delete;
} )
.Select( a => a.Target );
```

In the above snippet we are retrieving an advisory that is the list of proposed actions that the memento service think we should do to align the in memory model with a persistent storage. We are expecting that in the list of the tracked entities there is an `Address` class type and we are filtering Address instances looking only for items that should be deleted, that means that have been removed from a memento collection.

Since the above snippet is not really handy we set up a bunch of extension methods for the ChangeTrackingService component to support what we think are the most interesting use cases:

* Extensions are defined in the `Topics.Radical.ChangeTracking` namespace;
* `GetNewItems<T>()` retrieve the list of added items of the given type T;
* `GetChangedItems<T>()` retrieve the list of changed items of the given type T that were already exiting;
* `GetDeletedItems<T>()` retrieve the list of deleted items of the given type T; 
* `GetRemovedItems<T>()` retrieve the list of removed items of the given type T;

Using extension methods the above snippet can be rewritten as:

```csharp
var items = service.GetDeletedItems<Address>();
```

### Deleted and Removed items

What is the difference between deleted items and removed items?

* `Deleted` items are items that we initially load into the collection, we can call them persistent, that were removed during the editing phase;
* `Removed` items are items that were created during the editing phase, transient items, and then removed from the collections, most of the time this type of items can be safely ignored since their existence do not affect the persistent storage;