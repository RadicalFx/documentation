using System.Windows;

namespace AutoCommandBindingWithNestedSourceSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.AutoCommandBindingWithNestedSourceView>();
        }
    }
}
