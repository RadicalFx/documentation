We have already discussed how to handle change tracking in [[collections|Handling change tracking in collections]] and in complex [[models|Handling change tracking in complex objects graph]] and we have introduced how to handle change tracking in a [[MVVM based model|Handling change tracking in a simple viewmodel]].

We want to start where we left adding a collection to the `Person` class and setup the entire editing pipeline for the collection too.

```csharp
class Person{
    public Person()    {
        this.Addresses = new List<Address>();
    }
    
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public IList<Address> Addresses { get; private set; }
}

class Address{
    public String Street { get; set; }
    public String City { get; set; }
}
```

If we look at the [[considerations we did for the simple view model|Handling change tracking in a simple viewmodel]] it is obvious that the `Address` class itself needs a `ViewModel` and an editor and also the collection exposed by the `Person` class needs an editor and potentially a `ViewModel` depending on the type of editing that we want to support.

We need to face a couple of more issues related to the fact that having one graph coming from the persistent storage and one different graph bound to the UI we need to keep them in sync.

The `AddressViewModel` will be as simple as the PersonViewModel we already saw:

```csharp
class AddressViewModel : MementoEntity{    public void Initialize( Address address, Boolean registerAsTransient )    {        if( registerAsTransient )        {            this.RegisterTransient();        }        this.SetInitialPropertyValue( () => this.Street, address.With( a => a.Street ).Return( s => s, "" ) );        this.SetInitialPropertyValue( () => this.City, address.With( a => a.City ).Return( c => c, "" ) );    }    public String Street    {
        get { return this.GetPropertyValue( () => this.Street ); }
        set { this.SetPropertyValue( () => this.Street, value ); }    }    public String City    {        get { return this.GetPropertyValue( () => this.City ); }        set { this.SetPropertyValue( () => this.City, value ); }    }}
```

Nothing new, except for the `With`/`Return` syntax that is simply a `monad` like way to guard against `null` adding a default value.

Things get much more interesting as we look at the `PersonViewModel`, that revisited, now handle the `Addresses` list:

```csharp
public class PersonViewModel : MementoEntity{
    MementoEntityCollection<AddressViewModel> addressesDataSource;    public void Initialize( Person person, Boolean registerAsTransient )    {
        if( registerAsTransient )
        {            this.RegisterTransient();        }
        
        this.SetInitialPropertyValue( () => this.FirstName, person.FirstName );        this.SetInitialPropertyValue( () => this.LastName, person.LastName );        this.addressesDataSource = new MementoEntityCollection<AddressViewModel>();        this.addressesDataSource.BulkLoad( person.Addresses, a =>        {
            return this.CreateAddressViewModel( a, registerAsTransient );
        } );        this.Addresses = this.addressesDataSource.DefaultView;        this.Addresses.AddingNew += ( s, e ) =>        {
            e.NewItem = this.CreateAddressViewModel( null, true );            e.AutoCommit = true;        };    }    AddressViewModel CreateAddressViewModel( Address a, Boolean registerAsTransient )    {
        var vm = new AddressViewModel();        vm.Initialize( a, registerAsTransient );
        return vm;
    }    protected override void OnMementoChanged( IChangeTrackingService newMemento, IChangeTrackingService oldMemento )
    {        base.OnMementoChanged( newMemento, oldMemento );        if( oldMemento != null )        {
            oldMemento.Detach( this.addressesDataSource );        }        if( newMemento != null )        {            newMemento.Attach( this.addressesDataSource );        }    }    public String FirstName    {        get { return this.GetPropertyValue( () => this.FirstName ); }        set { this.SetPropertyValue( () => this.FirstName, value ); }    }    public String LastName
    {        get { return this.GetPropertyValue( () => this.LastName ); }        set { this.SetPropertyValue( () => this.LastName, value ); }
    }    public IEntityView<AddressViewModel> Addresses    {        get;        private set;    }}
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
class EditorViewModel : AbstractViewModel{    readonly IChangeTrackingService service = new ChangeTrackingService();    public EditorViewModel()    {        var observer = MementoObserver.Monitor( this.service );        this.UndoCommand = DelegateCommand.Create()            .OnCanExecute( o => this.service.CanUndo )            .OnExecute( o => this.service.Undo() )            .AddMonitor( observer );        this.RedoCommand = DelegateCommand.Create()            .OnCanExecute( o => this.service.CanRedo )            .OnExecute( o => this.service.Redo() )            .AddMonitor( observer );        this.CreateNewAddressCommand = DelegateCommand.Create()            .OnExecute( o =>             {                this.SelectedAddress = this.Entity.Addresses.AddNew();            } );        this.DeleteAddressCommand = DelegateCommand.Create()            .OnCanExecute( o => this.SelectedAddress != null )            .OnExecute( o =>             {                this.SelectedAddress.Delete();                this.SelectedAddress = this.Entity.Addresses.FirstOrDefault();            } )            .AddMonitor( PropertyObserver.For( this ).Observe( v => v.SelectedAddress ) );        var person = new Person()        {            FirstName = "Mauro",            LastName = "Servienti"        };        person.Addresses.Add( new Address( person )        {            City = "My town",            Street = "Where I live"        } );        var entity = new PersonViewModel();        entity.Initialize( person, false );        this.service.Attach( entity );        this.Entity = entity;    }    public ICommand UndoCommand { get; private set; }    public ICommand RedoCommand { get; private set; }    public ICommand CreateNewAddressCommand { get; private set; }    public ICommand DeleteAddressCommand { get; private set; }    public PersonViewModel Entity
    {        get { return this.GetPropertyValue( () => this.Entity ); }        private set { this.SetPropertyValue( () => this.Entity, value ); }    }    public IEntityItemView<AddressViewModel> SelectedAddress    {        get { return this.GetPropertyValue( () => this.SelectedAddress ); }        private set { this.SetPropertyValue( () => this.SelectedAddress, value ); }    }}
```