using System.Windows;

namespace FocusSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.FocusView>();
        }
    }
}
