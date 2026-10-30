using Radical.Windows.Presentation;

namespace ThirdPartyContainer.Presentation
{
    class MainViewModel : AbstractViewModel
    {
        public string Description { get; } = "This sample uses Autofac as an external container through generic host support, all the relevant bits are the in App class constructor.";
    }
}