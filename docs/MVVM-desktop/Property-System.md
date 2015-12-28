WPF has a really nice feature called Dependency Property, from the user perspective a dependency property is a standard CLR property that add, on top of CRL properties, a set of really nice and powerful features:

1. Property value inheritance;
2. Property metadata;
3. Property change notification;
4. Support for default value generation;
5. *…and many others strictly related to WPF;*

The [Radical](https://github.com/RadicalFx/radical) assembly where the property system lives is totally non-related to WPF in any way, we have simply decided to bring the power of dependency-like properties in order to give some interesting boost to certain part of the Radical framework.

One really interesting thing of the dependency properties, the WPF ones, is that values and metadata are stored at the root object level, we inherited that concept in our `Entity` base abstract class; lots of Radical stuff inherit from the `Entity` base class so to obtain something really interesting:

```csharp
class MyObject : Entity
{
    public String MyProperty
    {
        get{ return this.GetPropertyValue( () => this.MyProperty ); }
        set{ this.SetPropertyValue( () => this.MyProperty, value ); }
    }
}
```

what we see here is what we call a Radical property (RP), that from the outside is viewed, and behaves, like a standard CLR property but, from the inside, is totally managed by the `Entity` base class, and in our object we only expose a property.

##Property change notification

The first thing we get using a Radical property is property change notification, the base `Entity` class implements `INotifyPropertyChanged` and automatically fires the event whenever the property really changes; really means that subsequently setting the same value more than once fires the event only the first time.

##Property Metadata

Since we have everything managed by the base class, thus the base class holds all the properties and property values we can easily  introduce the concept of `csharp` attached to a property without requiring the inheriting class to do nothing:

```csharp
class MyObject : Entity
{
    public MyObject()
    {
        var metadata = this.GetPropertyMetadata( () => this.MyProperty );
    }

    public String MyProperty
    {
        get { return this.GetPropertyValue( () => this.MyProperty ); }
        set { this.SetPropertyValue( () => this.MyProperty, value ); }
    }
}
```

We are retrieving the default property metadata for the given property, using metadata the first thing we can do is to define the property default value.

##Default Value

The property default value is requested the first time a property get is issued, we will use the property metadata to define the default value for a property because we do not want to trigger a PropertyChanged event for the simple fact of defining a default, initial value:

```csharp
public MyObject()
{
    var metadata = this.GetPropertyMetadata( () => this.MyProperty );

    metadata.DefaultValue = "this is the default value";
}
```

but much more interesting is the possibility to intercept the default value request using a lambda:

```csharp
public MyObject()
{
    var metadata = this.GetPropertyMetadata( () => this.MyProperty );

    metadata.DefaultValueInterceptor = () => "this is the default value";
}
```

so to be able to perform some logic when the default value is requested. Both approaches can be used in a fluent manner:

```csharp
public MyObject()
{
    this.GetPropertyMetadata( () => this.MyProperty )
        .WithDefaultValue( "this is the default value" );
}

public MyObject()
{
    this.GetPropertyMetadata( () => this.MyProperty )
        .WithDefaultValue( () => "this is the default value" );
}
```

##Cascade changes

once we have property metadata we can add some interesting features such as cascade change notifications:

```csharp
class MyObject : Entity
{
    public MyObject()
    {
        this.GetPropertyMetadata( () => this.MyProperty )
            .AddCascadeChangeNotifications( () => this.AnotherProperty );
    }

    public String MyProperty
    {
        get { return this.GetPropertyValue( () => this.MyProperty ); }
        set { this.SetPropertyValue( () => this.MyProperty, value ); }
    }

    public Int32 AnotherProperty
    {
        get { return 0 /* e.g. runtime evaluated property */; }
    }
}
```

in this sample each time the MyProperty changes the PropertyChanged event is raised even for the AnotherProperty property. The RemoveCascadeChangeNotifications can be used to remove a cascade change notification previously added.

##Disable change notifications

by default all the radical properties notify of their change, if we want to disable change notifications for a specific property we’ll use once again property metadata:

```csharp
public MyObject()
{
    this.GetPropertyMetadata( () => this.MyProperty )
        .DisableChangesNotifications();
}
```

at a later time changes can be re-enabled using the EnableChangeNotifications method.

##Change detection

In the case we need to detect the change of a property from within the object itself we can use property metadata:

```csharp
public MyObject()
{
    this.GetPropertyMetadata( () => this.MyProperty )
        .OnChanged( pvc => 
        {
            //invoked whenever the property changes
        } );
}
```

or directly interact with the property definition:

```csharp
public String MyProperty
{
    get { return this.GetPropertyValue( () => this.MyProperty ); }
    set { this.SetPropertyValue( () => this.MyProperty, value, pvc => 
    {
        //invoked whenever the property changes
    } ); }
}
```

in both cases we get access to the current property value and to old property value.