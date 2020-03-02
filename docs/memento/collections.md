## Handling change tracking in collections

We briefly introduced [MementoEntity and MementoEntityCollection](memento-entities.md) and seen how to [track changes in a simple graph](simple-model.md).

We can go further and introduce collection change tracking:

```csharp
var list = new MementoEntityCollection<String>();
memento.Attach(list);

list.Add("a");
list.Add("b");
list.Add("c");

var count = list.Count; //3

memento.Undo();
//list.Count 2

memento.Redo();
//list.Count 3
```

The `MementoEntityCollection<T>` being a memento entity will keep track of changes applied to collection structure. Each change will be tracked: `Add`, `Remove`, `Clear`, `Insert`, `InsertAt`, etc...

```csharp
var list = new MementoEntityCollection<Person>();
memento.Attach(list);

list.Add(new Person());
```

Adding a `MementoEntity`, such as `Person`, will automatically trigger the memento that will start tracking the `Person` instance:

```csharp
var list = new MementoEntityCollection<Person>();
memento.Attach( list );

var person = new Person();
list.Add( person );

person.FirstName = "name";

memento.Undo(); //the person first name is reverted
memento.Undo(); //the person instance is removed from the collection
memento.Redo(); //the person instance is added once again to the collection
```

As expected the changes stack is handled in the correct order. The next step is to understand how to [handle change tracking in complex objects graph](complex-graph.md).
