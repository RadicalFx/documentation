Dealing with complex graphs of objects can be complicated and can get more complicated as the graph evolve or gets huge.

Let us start from the end of the story, what we want to achieve in our software solutions is something like the following sample code:

```csharp
var person = new Person();
person.FirstName = "first name value";
person.LastName = "last name value";

var address =  new Address();
address.Street = "street address value";

person.Addresses.Add( address );
```

Given the above code snippet we have basically 2 requirements:

* Know the state of the graph:
    * Is it changed?
    * Is there something that can be undone?
    * Is there something that can be redone?
* Change the state of the graph:
    * Accept all changes at once;
    * Reject all changes at once;
    * Undo a single change;
    * Redo a single change; 

But there is more, from the user perspective a single change can be reflected in more than one action, and thus change, in the code itself:

```csharp
var order = new Order();
order.Customer = ... //reference to a customer object;

// --> begin of "atomic" operation
var item = new OrderItem();
item.ItemId =  123;
item.Quantity = 2;
order.Items.Add( item );
// --> end of "atomic" operation
```

In the above sample the creation of the order item, the set of its properties and the add to the items collection, from the user perspective, are a single operation that matches the `add to cart` operation, given this assumptions an undo operation should rollback the entire change set and only the last operation, the add in this case. 

Given these requirements the next step is to rely on something that allows us to transparently handle the entire change tracking process, the first step is to understand what [[MementoEntity and MementoEntityCollection]] are.