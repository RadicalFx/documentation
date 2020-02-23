## MementoEntity and MementoEntityCollection

In [Change Tracking Service](change-tracking-service.md) we introduced a code snippet as the following:

```csharp
var person = new Person();
person.FirstName = "first name value";
person.LastName = "last name value";
```

To start using memento services we can extend the above snippet as follows:

```csharp
var memento = new ChangeTrackingService();

var person = new Person();
memento.Attach(person);

person.FirstName = "first name";
person.LastName = "last name";

var isChanged = memento.IsChanged; //true
var canUndo = memento.CanUndo; //true
```

Calling `memento.Undo()` will trigger the undo of the last operation, we can call `Undo` until `CanUndo` is `true` rolling back changes change by change. In the above sample calling `Undo` will revert back the `LastName` property value to its default value.

The requirement to achieve the above is that the `Person` class is a Radical _memento_ entity:

```csharp
class Person : MementoEntity
{   
    public String FirstName
    {
        get { return this.GetPropertyValue(() => this.FirstName); }
        set { this.SetPropertyValue(() => this.FirstName, value); }
    }

    public String LastName
    {
        get { return this.GetPropertyValue(() => this.LastName); }
        set { this.SetPropertyValue(() => this.LastName, value); }
    }
}
```

As we can see from the above snippet all we need to do is to create a class the inherits from the `MementoEntity` base class and declare all the properties we want to track as [Radical properties](/entities/property-system.md).

A similar approach can be used to keep track of items in a collection:

```csharp
var memento = new ChangeTrackingService();

var list = new MementoEntityCollection<String>();
memento.Attach(list);

list.Add("a value");
list.Add("another value");

var isChanged = memento.IsChanged; //true
var canUndo = memento.CanUndo; //true
```

Calling `memento.Undo()` will trigger the undo of the last operation that in the above sample will revert the collection status removing the last added value.

We can do more:

* [Handling change tracking in a simple model](simple-model.md);
* [Handling change tracking in collections](collections.md);
* [Handling change tracking in complex objects graph](complex-graph.md);
