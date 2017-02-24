using MyApp.Contracts.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Radical.Windows.Presentation;

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
