## AbstractMementoViewModel and the ChangeTrackingService

The [AbstractViewModel](abstract-view-model.md) base class provides us a way to create `ViewModels` with a set of base features that satisfies most of the basic requirements.

When dealing with complex MVVM based application we sometimes need to deal with the user editing graph of objects, changing property values and/or adding/removing items from and to collections; the end user is generally used to editors, such as Microsoft Word, that provides rich editing features with Undo/Redo support.

Implementing Undo/Redo like features is not as simple as it can appear in the first place, Radical supports a feature called `Memento`, based on the memento pattern, that allows us to easily implement a change tracking system with fine grain control over what is going on and with a rich set of features out of the box.

The first, and easy, step to start using `Memento` is to inherit our `ViewModels` from the `AbstractMementoViewModel` class:

```csharp
class MainViewModel : AbstractMementoViewModel
{

}
```

The above code immediately enrich our `ViewModel` with change tracking capabilities, nothing else needs to be done in order to implement a basic Undo/Redo support in the ViewModel except writing properties using the Radical [Property System](/entities/property-system.md).

Given that an object graph can be complex and shaped as we like we need a single entry point to achieve at least two goals:

* Access the current state of the graph;
* Control the state of the graph;

The one component to rule both aspects is the [Change Tracking Service](/memento/change-tracking-service.md). The next step is to create a `ChangeTrackingService` instance to track the state of the model.

```csharp
class MainViewModel : AbstractMementoViewModel
{
    public MainViewModel()
    {
        var memento = new ChangeTrackingService();
        memento.Attach( this );	
    }
}
```

We created a new instance of the memento service and instructed it to keep track of changes that will occur to `this` instance.

Once we setup the memento we can access the state of the graph via its properties such as `IsChanged`, `CanUndo` and `CanRedo`, or we can control the state of the graph via the exposed methods, such as, but not only, `AcceptChanges()`, `RejectChanges()`, `Undo()` or `Redo()`.

As we said in order to allow a transparent tracking we need to leverage the power of the Radical property system, using properties as the following will immediately trigger the memento and will start keeping track of changes:

```csharp
public String Text
{
    get { return this.GetPropertyValue( () => this.Text ); }
    set { this.SetPropertyValue( () => this.Text, value ); }
}
```

One thing to keep in mind is that every time we write to the property, once the graph is attached to the memento, that write operation will be tracked:

```csharp
class MainViewModel : AbstractMementoViewModel
{
    public MainViewModel()
    {
        var memento = new ChangeTrackingService();
        memento.Attach( this );
        
        this.Text = "text property default value";
    }
    
    public String Text
    {
        get { return this.GetPropertyValue( () => this.Text ); }
        set { this.SetPropertyValue( () => this.Text, value ); }
    }
}
```

Setting the `Text` property default/initial value in the above sample will trigger the `ChangeTrackingService` that now reports its state as changed: `IsChanged` equals `true`.

In the above minimalistic sample it is obvious that the easiest solution is to set the property value *before* attaching the graph to the memento, but this is not always possible:

```csharp
class MainViewModel : AbstractMementoViewModel
{
    public MainViewModel()
    {
        var memento = new ChangeTrackingService();
        memento.Attach( this );
        
        this.SetInitialPropertyValue( () => Text, "text property default value" );
    }
    
    public String Text
    {
        get { return this.GetPropertyValue( () => this.Text ); }
        set { this.SetPropertyValue( () => this.Text, value ); }
    }
}
```

The `SetInitialPropertyValue`  method is aware of the fact that a memento can listen to changes and it won't trigger any change in the state.

Note: the `SetInitialPropertyValue` is a shortcut to access the metadata of the `Text` property, it is exactly the same as:

```csharp
this.GetPropertyMetadata( () => this.Text )
    .WithDefaultValue( "text property default value" );
```

What's next:

* dive into the [Change Tracking Service](/memento/change-tracking-service.md) component.
* Understand how to handle change tracking in [simple ViewModels](memento-change-tracking-simple-view-model.md), [complex ones and collections](memento-change-tracking-collection-and-complex-view-model.md.md).

### Frequently Asked Questions

**Q**: Is `AbstractMementoViewModel` required?  
*A*: No, it is not required, it is handy. A memento entity is required to be a `IMemento` instance, the easiest way to implement a memento entity is to inherit from `MementoEntity`, that since it implements `INotifyPropertyChanged` is it enough to partecipate in the MVVM data binding process. Inheriting from `AbstractMementoViewModel` adds more features such as automatic validation support.

**Q**: Why isn't the `AbstractMementoViewModel` providing me a `ChangeTrackingService` instance?  
*A*: Because there is no 1:1 match between an edited entity and a tracking service, most of the time a single tracking service will track more than one entity at a time.