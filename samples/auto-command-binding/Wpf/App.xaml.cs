using System.Windows;

namespace AutoCommandBindingSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.AutoCommandBindingView>();
        }
    }
}
