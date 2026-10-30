using System.Windows;

namespace AutoCommandBindingWithTemplatesSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.AutoCommandBindingWithTemplatesView>();
        }
    }
}
