We briefly introduced [[MementoEntity and MementoEntityCollection]] and seen how to [[track changes in a simple graph|Handling change tracking in a simple model]].

We can go further and introduce collection change tracking:

```csharp
var list = new MementoEntityCollection<String>();
```

The `MementoEntityCollection<T>` being a memento entity will keep track of changes applied to collection structure. Each change will be tracked, starting from `Add` and `Remove` to `Clear`, `Insert`, `InsertAt`, etc...

```csharp
var list = new MementoEntityCollection<String>();
```

Adding a `MementoEntity`, such as `Person`, will automatically trigger the memento that will start tracking the `Person` instance:

```csharp
var list = new MementoEntityCollection<String>();

person.FirstName = "name";

memento.Undo(); //the person first name is reverted
memento.Undo(); //the person instance is removed from the collection
memento.Redo(); //the person instance is added once again to the collection
```

As expected the changes stack is handled in the correct order. The next step is to understand how to [[handle change tracking in complex objects graph|Handling change tracking in complex objects graph]].