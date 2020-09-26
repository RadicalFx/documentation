## Expose ViewModels as Views resources

There are scenarios in which it could be handly to have the current View ViewModel being registered also as a View resource. ViewModels can be exposed as a View resource via the `ExposeViewModelAsStaticResource` [runtime convention](runtime-conventions.md) or by applying the `ExposeViewModelAsStaticResource` attribute to the ViewModel class definition.

It's possible to change the way resource keys are generated for exposed ViewModels via the `GenerateViewModelStaticResourceKey` [convention](runtime-conventions.md)
