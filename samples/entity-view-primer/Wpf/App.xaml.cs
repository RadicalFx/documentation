using System.Windows;

namespace EntityViewPrimerSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.HelloWorldView>();
        }
    }
}
