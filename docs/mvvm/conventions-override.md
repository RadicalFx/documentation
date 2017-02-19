## Conventions override

Radical is conventions based, we have [runtime conventions](/../runtime-conventions) and [bootstrap conventions](../bootstrap-conventions) in order to facilitate the conventions override, to replace or integrate a default behavior we now support the concept of default conventions.

Originally the code to write, still supported, to override a convention was something like the the following:

```csharp
var original = conventions.IsViewModel;
conventions.IsViewModel = type => 
{
    if ( type.Namespace == "MyViewModelsNamespace" ) 
    {
        return true;
    }
    return original( type );
};
```

We now support for both [[runtime conventions]] and [[bootstrap conventions]] the following syntax:

```csharp
conventions.IsViewModel = type => 
{
    if ( type.Namespace == "MyViewModelsNamespace" ) 
    {
        return true;
    }
    return conventions.DefaultIsViewModel( type );
};
```

For every convention we have there is a convention whose name is the same but prefixed with `Default` so that we are not required anymore to keep track of the original convention we are overriding.