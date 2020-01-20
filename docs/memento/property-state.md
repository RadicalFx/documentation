## Entity Property State

Available in `Radical` starting `v2.0.0`.

When dealing with change tracking systems it is important to know the state of the graph from 2 different point of views

* System point of view
* User point of view

The Radical [`ChangeTrackingService`](change-tracking-service.md) allows to gather both point of view with property level granularity.

Given a `Person` entity with a `Name` property we can basically be in 2 major scenarios:

```csharp
var person = new Person( name: "Mauro" );

var memento = new ChangeTrackingService();
memento.Attach( person );

person.Name = "Giovanni"
```

Or something a bit more problematic:

```csharp
var person = new Person( name: "Mauro" );

var memento = new ChangeTrackingService();
memento.Attach( person );

person.Name = "Giovanni"

//some other changes

person.Name = "Mauro"
```

In both cases from the `ChangeTrackingService` perspective the `Person` instance is changed, however from the user perspective maybe it's not since the `Name` property has the exact same value as when it was instantiated.

The `GetPropertyState( entity, property )` method of `ChangeTrackingService` instance, given a tracked entity and a property name, returns the current state of the property as seen by the tracking service.

The state can be:

* `None `:  The property has no tracked states, thus is not changed either;

*   `Changed`:  The property has tracked states, thus has changes; This means that in some way the property changed over time, this state does not take into account the actual value of the property;
*   `ValueChanged`:  The property has tracked states and the actual value is different from the original one;

Looking at the above snippets, the first one will be:

```csharp
var state = memento.GetPropertyState( person, p => p.Name );
```

state will be `Changed` and `ValueChanged`. On the contrary the second one will only `Changed` because the memento service has tracked some changes but the actual value is the same as the original one.

#### Extensions

In order to simply the query there is also an extension method for `IMemento` entities:

*    `IsPropertyValueChanged( property )`: Is an extension method to `IMemento` entities that acts like a shortcut to if a property value changed over time or not;

Sample usage

```csharp
var person = new Person( name: "Mauro" );

var memento = new ChangeTrackingService();
memento.Attach( person );

person.Name = "Giovanni"
  
var isNameValueChanged = person.IsPropertyValueChanged( p => p.Name ); //will be true
```

