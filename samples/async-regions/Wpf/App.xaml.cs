using System.Windows;

namespace AsyncRegionsSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.AsyncRegionsView>();
        }
    }
}
