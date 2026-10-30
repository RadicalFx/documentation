using System.Windows;

namespace CueBannerSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.CueBannerSampleView>();
        }
    }
}
