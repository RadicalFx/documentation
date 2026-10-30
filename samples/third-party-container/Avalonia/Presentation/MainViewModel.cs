using Radical.Avalonia.Presentation;

namespace ThirdPartyContainer.Presentation
{
    class MainViewModel : AbstractViewModel
    {
        public string Description { get; } = "This sample uses Autofac as an external container through generic host support, all the relevant bits are in the App class OnFrameworkInitializationCompleted method.";
    }
}
