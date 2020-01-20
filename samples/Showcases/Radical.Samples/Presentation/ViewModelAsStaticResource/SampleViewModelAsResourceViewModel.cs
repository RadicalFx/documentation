using Radical.Samples.ComponentModel;

namespace Radical.Samples.Presentation.ViewModelAsStaticResource
{
    [Sample(Title = "ViewModel as Resource", Category = Categories.Presentation)]
    public class SampleViewModelAsResourceViewModel : SampleViewModel
    {
        public SampleViewModelAsResourceViewModel()
        {
            Foo = "Hi, there. From dynamic resources!";
        }

        public string Foo { get; set; }
    }
}
