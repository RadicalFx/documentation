We have already discussed how to handle change tracking in [[collections|Handling change tracking in collections]] and in complex [[models|Handling change tracking in complex objects graph]] and we have introduced how to handle change tracking in a [[MVVM based model|Handling change tracking in a simple viewmodel]].

We want to start where we left adding a collection to the `Person` class and setup the entire editing pipeline for the collection too.

```csharp
class Person
    public Person()
        this.Addresses = new List<Address>();
    }
    
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public IList<Address> Addresses { get; private set; }
}

class Address
    public String Street { get; set; }
    public String City { get; set; }
}
```

If we look at the [[considerations we did for the simple view model|Handling change tracking in a simple viewmodel]] it is obvious that the `Address` class itself needs a `ViewModel` and an editor and also the collection exposed by the `Person` class needs an editor and potentially a `ViewModel` depending on the type of editing that we want to support.

We need to face a couple of more issues related to the fact that having one graph coming from the persistent storage and one different graph bound to the UI we need to keep them in sync.

The `AddressViewModel` will be as simple as the PersonViewModel we already saw:

```csharp
class AddressViewModel : MementoEntity
        get { return this.GetPropertyValue( () => this.Street ); }
        set { this.SetPropertyValue( () => this.Street, value ); }
```

Nothing new, except for the `With`/`Return` syntax that is simply a `monad` like way to guard against `null` adding a default value.

Things get much more interesting as we look at the `PersonViewModel`, that revisited, now handle the `Addresses` list:

```csharp
public class PersonViewModel : MementoEntity
    MementoEntityCollection<AddressViewModel> addressesDataSource;
        if( registerAsTransient )
        {
        
        this.SetInitialPropertyValue( () => this.FirstName, person.FirstName );
            return this.CreateAddressViewModel( a, registerAsTransient );
        } );
            e.NewItem = this.CreateAddressViewModel( null, true );
        var vm = new AddressViewModel();
        return vm;
    }
    {
            oldMemento.Detach( this.addressesDataSource );
    {
    }
```

We are using a `MementoEntityCollection<T>` to keep track of changes that occurs to the collection structure, such as add or address removal, we are using the `BulkLoad` API to achieve 2 goals:

1. Add a transformation on load, we are basically iterating over `Address` instances adding to the collection `AddressViewModel` instances, and the transformation is done in the delegate via the `CreateAddressViewModel` that simply wraps the `Address` instance, if any, into the `AddressViewModel` instance initializing it as we saw for the `Person` / `PersonViewModel` relationship;
2. disable at once collection notifications, a `IEntityCollection<T>` has built-in support for changes notification, and a `MementoEntityCollection<T>` for change tracking, the `BulkLoad` API will disable notifications and tracking for the entire load process re-enabling both at the end;

We then expose our `Addresses` list as an `IEntityView` that is a `IBindingListView` implementation achieving 2 goals:

1. In the `View` we can now bind the collection to a `DataGrid`, for example, gaining full support for sorting, filtering and column generation;
2. We can have control, very easily, over new items generation even if the request is done by a `DataGrig` control: simply add a `EventHandler` to the `AddingNew` event of the `IEntityView` and create the expected instance; refer to the [[EntityView]] documentation for more details;

The last thing to do is to manually propagate the current `ChangeTrackingService` instance to the collection owned by the `PersonViewModel` class, we do that overriding the `OnMementoChanged` method that is called every time the current memento tracking this instance changes.

The last thing is to update the `EditorViewModel` to create a sample data set; we also add a couple of commands to manage the `Addresses` collection and a property to keep track of the currently selected address:

```csharp
class EditorViewModel : AbstractViewModel
    {
```