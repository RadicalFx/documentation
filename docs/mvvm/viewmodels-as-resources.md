## Expose ViewModels as Views resources

There scenarios in which it could be handly to have the current View ViewModel being registered also as a View resource. ViewModels can be exposed as a View resource via the `ExposeViewModelAsStaticResource` [runtime convention](runtime-conventions.md). The default behavior is that ViewModels are never exposed as resources.

It's possible to change the way resource keys are generated for exposed ViewModels via the `GenerateViewModelStaticResourceKey` [convention](runtime-conventions.md)
