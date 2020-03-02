---
layout: default
toc: mvvm-toc.html
---

## Conventions override

Radical is conventions based, [runtime conventions](runtime-conventions.md) and [bootstrap conventions](bootstrap-conventions.md). To facilitate conventions override, to replace or integrate a default behavior the concept of default conventions is supported.

Both [runtime conventions](runtime-conventions.md) and [bootstrap conventions](bootstrap-conventions.md) support the following syntax:

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

For every convention there is a convention whose name is the same but prefixed with `Default*` so that it's not anymore required to keep track of the original convention we are overriding.
