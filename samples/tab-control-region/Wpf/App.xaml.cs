using System.Windows;

namespace TabControlRegionSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.TabControlRegionView>();
        }
    }
}
