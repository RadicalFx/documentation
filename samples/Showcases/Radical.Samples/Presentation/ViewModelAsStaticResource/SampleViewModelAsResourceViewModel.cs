using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Radical.ComponentModel;
using Topics.Radical.Windows.Presentation.ComponentModel;

namespace Topics.Radical.Presentation.ViewModelAsStaticResource
{
    [Sample(Title = "ViewModel as Resource", Category = Categories.Presentation)]
    public class SampleViewModelAsResourceViewModel : SampleViewModel
    {
        public SampleViewModelAsResourceViewModel()
        {
            this.Foo = "Hi, there. From dynamic resources!";
        }

        public string Foo { get; set; }
    }
}
