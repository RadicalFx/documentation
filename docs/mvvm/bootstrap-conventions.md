---
layout: default
toc: mvvm-toc.html
---

## Bootstrap Conventions

As we have already said the whole bootstrap process is completely based on conventions, especially the IoC container setup. Bootstrap conventions are mainly related to the way components are registered into the container:

* every class that is defined in a namespace ending with `Services (*.Services)` will be considered a service and will be registered as `singleton` using as the service contract the first interface, if any, otherwise using the class type;
* every class that is defined in a namespace ending with `Presentation (*.Presentation)` and whose type name ends with `ViewModel (*ViewModel)` will be considered as a view model and registered as transient;
  * following the same logic every type in the same namespace whose name ends with `View (*View)` will be considered a view, a transient view;
  * if a view or a view model are a shell, type name beginning with `Shell*` or `Main*`, they will be registered as singleton
  * be default views and view models will be registered using as service contract the class type and no interface is searched along the way;
* every type defined in a namespace ending with `Messaging.Handlers (*.Messaging.Handlers)` will be considered a message broker message handler and will be registered as singleton and automatically attached, as an handler, to the broker pipeline;

These are the main conventions used at boot time, there are a few more but less important. Obviously all these behaviors can be replaced or extended to accomplish the end user needs:

```csharp
public partial class App : Application
{
    public App()
    {
        this.AddRadicalApplication<MainView>(configuration => 
        {
           configuration.BootstrapConventions.IsViewModel = type => 
           {
              if (type.Namespace == "MyViewModelsNamespace") 
              {
                 return true;
              }

              return configuration.BootstrapConventions.DefaultIsViewModel(type);
            };
         });
    }
}
```

In the above sample we are integrating the conventions used to determine if a type is a view model.
