## Intercept ViewModels before they are used

One of the typical scenario in a desktop application is the requirement to “open” a new view passing to the newly created view/view model some data, the ideal solution is also to be able to pass data to the constructor of the new view model in order to be sure that at the end of the construction process everything is correctly setup.

When dealing with Inversion of Control framework this is generally a pain point because each framework provides ways to achieve the goal, but generally the solution is really weak and full of pain points.

In order to solve the above problem the general approach is something like the following:

```csharp
var viewModel = myFavoriteIoC.Resolve();
viewModel.Initialize(someData);
```

In a view first scenario, like the one proposed by default by Radical this is not so easy because you end up with the following code:

```csharp
var view = viewResolver.GetView<MyView>();
var viewModel = conventions.GetViewDataContext( view ) as MyViewModel;
viewModel.Initialize( someData );
```

it works, using the built-in [conventions](../mvvm/runtime-conventions.md) we retrieve an instance of the attached view model and set it up, but the problem is that the setup occurs after that the view model has been wired to the view, and in some cases this is not ideal.

## Interceptors

The Radical [view resolver](../mvvm/iview-resolver.md) lets the user intercept the view model before it is wired up to the view:

```csharp
var view = viewResolver.GetView<MyView>(vm => 
{
  //do what you want with the view model
});
```

the `Action` that intercept the view model is called before the view model is attached to the view, in the above sample, since we cannot infer the view model type, the view model is passed to the action as `object`, if you know upfront the type of the view model you can explicit tell Radical what it is:

```csharp
var view = viewResolver.GetView<MyView, MyViewModel>(vm => 
{
  //do what you want with the view model
});
```

And the delegate will be of type `Action<MyViewModel>`.
