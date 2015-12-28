When dealing with data editing and the MVVM pattern we need to be aware that the shortest path from the model to the UI is not always the best solution.

Imagine a scenario where we want to edit a `Person` instance that is loaded from a persistente storage, such as a database, the `Person` instance can be directly bound to the UI but it requires us to implement the `INotifyPropertyChanged` interface and if we want to enable it for the `ChangeTrackingService` we need to inherit from a base class.  
Both are not an option when dealing with the Single Responsibility Principle and with POCO objects.

In the above scenario we need to introduce at least two more actors, other than the `Person` data model:

1. A `PersonViewModel` that will be responsible to enrich the Person with property change notification support and with change tracking capabilities;
2. An `EditorViewModel` that will allow a clean separation of responsibilities owning all the  relationship with the memento.

The second bullet is especially true when dealing with complex graph and/or with more than one tracked entity at the same time. Given a `Person` class like the following:

```csharp
class Person{
    public String FirstName { get; set; }
    public String LastName { get; set; }
}
```

We can create a `PersonViewModel` such as:

```csharp
class PersonViewModel : MementoEntity{
    public void Initialize( Person person, Boolean registerAsTransient )
    {
        if( registerAsTransient )
        {
            this.RegisterTransient();
        }
        
        this.SetInitialPropertyValue( () => this.FirstName, person.FirstName );
        this.SetInitialPropertyValue( () => this.LastName, person.LastName );
    }    public String FirstName    {        get { return this.GetPropertyValue( () => this.FirstName ); }        set { this.SetPropertyValue( () => this.FirstName, value ); }    }    public String LastName    {        get { return this.GetPropertyValue( () => this.LastName ); }        set { this.SetPropertyValue( () => this.LastName, value ); }    }}
```

The first thing is to build a memento-enabled facade, that can grow adding feature, to enable change tracking and property change notifications in a Person-like class.  
In the above sample the `PersonViewModel` and the `Person` class are basically the same, we can say that this is corner case, most of the time in real scenarios there will be a huge difference between the model and the editing view model.

We are introducing a `Initialize` method, for the sake of the sample we can do the same thing using a constructor, using a `Initialize` method allows us to easily resolve `PersonViewModel` instances using an inversion of control container without the need to deal with the currently edited `Person` runtime instance. At initialization time we are doing 2 important things:

1. calling the `RegisterTransient()` method of the base class to register the current instance as transient, if required; To dive into the meaning of a transient entity look at the [[Change Tracking Service API]];
2. using the `SetInitialPropertyValue()` method to initialize the default value of the `PersonViewModel` properties without affecting its tracking state;

Once we have setup our `ViewModel` we can build the editor:

```csharp
public class EditorViewModel : AbstractViewModel{
    readonly IChangeTrackingService service = new ChangeTrackingService();    public EditorViewModel()    {        var observer = MementoObserver.Monitor( this.service );        this.UndoCommand = DelegateCommand.Create()
            .OnCanExecute( o => this.service.CanUndo )            .OnExecute( o => this.service.Undo() )            .AddMonitor( observer );        this.RedoCommand = DelegateCommand.Create()            .OnCanExecute( o => this.service.CanRedo )            .OnExecute( o => this.service.Redo() )            .AddMonitor( observer );        var person = new Person()        {            FirstName = "Mauro",            LastName = "Servienti"        };        var entity = new PersonViewModel();        this.service.Attach( entity );        entity.Initialize( person, false );        this.Entity = entity;
    }

    public ICommand UndoCommand { get; private set; }    public ICommand RedoCommand { get; private set; }
    
    public PersonViewModel Entity    {        get { return this.GetValue( () => this.Entity ); }        private set { this.SetValue( () => this.Entity, value ); }    }
}
```

There is a lot going on here we are creating an editor and at first we setup our `ChangeTrackingService` instance, that in this specific sample is bound to the editor itself. In the constructor we are setting up a [[MementoObserver]] to watch the memento instance and we are binding that observer to 2 commands whose role is to expose Undo/Redo functionalities to the UI.  
Last we create a `Person` instance, in real scenarios the `Person` instance is expected to arrive from a persistent storage or a remote resource, we create the `PersonViewModel`, attach it to the memento service and finally initialize it with the person data source.

We finally expose both commands and the `PersonViewModel` instance to the `View`.