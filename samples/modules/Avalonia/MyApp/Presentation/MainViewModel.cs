using MyApp.Contracts.ComponentModel;
using Radical.Avalonia.Presentation;

namespace MyApp.Presentation
{
    class MainViewModel : AbstractViewModel, MyApp.Contracts.IMainViewModel
    {
        public MainViewModel(ISampleSharedService service)
        {
            service.MyProperty = "Some value";
        }
    }
}
