using System.Windows;

namespace OpenChildWindowSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.OpenChildWindowView>();
        }
    }
}
