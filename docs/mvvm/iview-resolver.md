## The IViewResolver

As we have already wrote when we spoke about [Runtime Conventions](runtime-conventions.md) Radical utilizes by default a view first approach, that even if is completely replaceable with a ViewModel first approach, must be understood.

The main and only entry point used to resolve views is `IViewResolver` interface whose role is to resolve a view instance given a view type:

```c#
IViewResolver service; //injected by the IoC engine
var viewUsingGenerics = service.GetView<SampleView>();
var viewUsingType = service.GetView( typeof( SampleView ) );
```

At runtime when the `GetView` method is called the default built-in view resolver does the following things:

1. goes to the IoC container and resolves an instance of the requested view type;
2. if the view already has a `DataContext` it assumes that the view is a singleton and has been already resolved once and immediately returns the resolved view;
3. Otherwise, using the conventions:
4. Using the `ResolveViewModelType` convention determines the type of the associated ViewModel;
5. Resolves, via the container, the ViewModel;
6. Set the relation View – ViewModel;
7. Set the ViewModel as the DataContext of the View;
8. Attaches to the View the required behaviors;
9. Exposes services registered to be exposed as resources in the View
10. Returns the view to the caller;

### How to use the IViewResolver in our application

The typical usage of the view resolver in the application is to open/show another view, the easiest way is to declare a dependency on the resolver in our component:

```c#
class SampleViewModel
{
    readonly IViewResolver service;

    public SampleViewModel( IViewResolver service )
    {
        this.service = service;
    }

    public void ShowAView()
    {
        var myView = this.service.GetView<MyView>();
        myView.Show();
    }
}
```

We are using the simplest possible approach in order to keep the sample complexity really low.

### Notes

* In the above sample we are violating the MVVM pattern because we are dealing with a view within the ViewModel, in the chapter related to the [MessageBroker](/messaging/message-broker.md) we’ll see how to avoid this mix.
* A view does not require a view model to work properly, the `IViewResolver` can resolve views that don't have view models;
