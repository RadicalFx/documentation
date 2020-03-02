## Atomic operations

When dealing with a change tracking system based on the memento pattern one of the complex problem we can face is that the way users perceive an operation as atomic is not the same as the application.

Imagine a scenario where we have a list of items, ordered by some criteria such as a date for example, and the system we are designing needs to allow the user to move items, the issue is that what from the user perspective is a single operation, a move, from the system perspective is a multiple operation, a move plus the update of all the other items to keep dates, for example, in sync.

In the above scenario a `Undo()` operation on the memento won't produce the expected result, unless we instruct the memento itself:

```csharp
var memento = new ChangeTrackingService();

var person = new Person();
memento.Attach(person);

using(var op = memento.BeginAtomicOperation())
{
    person.FirstName = "a name";
    person.LastName = "last name";

    op.Complete();
}
```

At the end of the atomic operation, when `Complete()` is called, the state of the `Person` instance is changed but the stack contains one single change that, if undone, will revert back both the `FirstName` and `LastName` properties.

One important thing to keep in mind is that an atomic operation is not limited to a single object, multiple tracked instances can partecipate in the same atomic operation.
