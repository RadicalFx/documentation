## Handling change tracking in a simple model

We briefly introduced [MementoEntity and MementoEntityCollection](memento-entities.md). The following code snipped allows to attach the memento tracking system to an entity:

```csharp
var memento = new ChangeTrackingService();

var person = new Person();
memento.Attach(person);

person.FirstName = "first name";
person.LastName = "last name";

var isChanged = memento.IsChanged; //true
var canUndo = memento.CanUndo; //true
```

What is happening is that each change performed on each tracked entity will be recorded by che `ChangeTrackingService`. The following will work as expected:

```csharp
var memento = new ChangeTrackingService();

var person = new Person();
var customer = new Customer();
memento.Attach(person);
memento.Attach(customer);

person.FirstName = "first name";
person.LastName = "last name";
customer.CompanyName = "sample company";

var isChanged = memento.IsChanged; //true
var canUndo = memento.CanUndo; //true
```

Tracking at the same time both the `Person` instance and the `Customer` instance. The following sample highlights how changes are tracked:

```csharp
var memento = new ChangeTrackingService();

var person = new Person();
memento.Attach(person);

person.FirstName = "a name";
person.LastName = "last name";

var isChanged = memento.IsChanged;
var state = memento.GetEntityState(person);

memento.Undo();
//person.LastName is null

memento.Redo();
//person.LastName is "last name"
```

The memento keeps tracks of a stack of changes in the exact same order they happened to the tracked models, each time `Undo()` is called the last change in the stack will be reverted and moved into the forward changes stack allowing the caller to call `Redo()` in order to apply it once again.

Calling `AcceptChanges()` on a memento instance will flush all the recorded changes, and the model is unchanged. Calling `RejectChanges()` will revert all the tracked models to their original state, or to the state we called `AcceptChanges()` last time.

The same applies also to collections: [Handling Change Tracking in collections](collections.md)
