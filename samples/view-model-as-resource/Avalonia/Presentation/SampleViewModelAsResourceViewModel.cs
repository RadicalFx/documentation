using Radical.Avalonia.Presentation;
namespace ViewModelAsResourceSample.Presentation
{
    public class SampleViewModelAsResourceViewModel : AbstractViewModel
    {
        public SampleViewModelAsResourceViewModel()
        {
            Foo = "Hi, there. From dynamic resources!";
        }

        public string Foo { get; set; }
    }
}
