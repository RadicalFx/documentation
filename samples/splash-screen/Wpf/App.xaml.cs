using Radical.Windows.Bootstrap;
using System.Threading.Tasks;
using System.Windows;

namespace SplashScreenSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.MainView>(configuration =>
            {
                configuration.EnableSplashScreen(new SplashScreenConfiguration()
                {
                    // simulates some startup work, the splash screen is shown until it completes
                    StartupAsyncWork = serviceProvider => Task.Delay(3000)
                });
            });
        }
    }
}
