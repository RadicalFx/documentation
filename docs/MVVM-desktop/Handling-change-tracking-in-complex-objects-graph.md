We briefly introduced [[MementoEntity and MementoEntityCollection]] and seen how to [[track changes in a simple graph|Handling change tracking in a simple model]] and in [[collections|Handling change tracking in collections]].

The last type of graph we want to be able to track is a complex graph, where at least one of the properties of the root tracked object is a memento entity or a memento collection itself:

```csharp
class Person : MementoEntity
{
    public Person()    {
        this.Addresses = new MementoEntityCollection<Address>();
    }
    public String FirstName
    {
        get { return this.GetPropertyValue( () => this.FirstName ); }
        set { this.SetPropertyValue( () => this.FirstName, value ); }
    }

    public String LastName
    {
        get { return this.GetPropertyValue( () => this.LastName ); }
        set { this.SetPropertyValue( () => this.LastName, value ); }
    }
    
    public IList<Address> Addresses { get; private set; }
}

class Address : MementoEntity
{   
    public String Street
    {
        get { return this.GetPropertyValue( () => this.Street ); }
        set { this.SetPropertyValue( () => this.Street, value ); }
    }
}
```

In the above sample the `Person` class has a property, `Addresses`, whose type is itself a memento entity, a `MementoEntityCollection<Address>` in this specific case. Using the following snippet:

```csharp
var memento = new ChangeTrackingService();

var person = new Person();
memento.Attach( person );
```

the `Addresses` collection is not automatically tracked, the memento does not know anything of the structure of the graph. We can update the `Person` class so to instruct the memento that also the `Addresses` collection needs to be tracked:

```csharp
class Person : MementoEntity
{
    protected override void OnMementoChanged( IChangeTrackingService newMemento, IChangeTrackingService oldMemento )    {        base.OnMementoChanged( newMemento, oldMemento );
        if( oldMemento != null ) 
        {
            oldMemento.Detach( this.Addresses );
        }
        if( newMemento != null ) 
        {            newMemento.Attach( this.Addresses );        }
        
    //rest of the Person class code}
```

We are intercepting the moment in which the `Person` instance is tracked by the memento service overriding the `OnMementoChanged` and we are manually propagating the memento to inner instances. It is important to remove, detach, the memento entity from the previous memento instance if any, a memento entity can be tracked by one memento only at a time.

Note: changes to a `MementoEntityCollection<T>` automatically propagates the memento service to list items if they are a memento entity.