using Avalonia;
using Avalonia.Markup.Xaml;
using Radical.Avalonia.Bootstrap;
using System.Threading.Tasks;

namespace SplashScreenSample
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            this.AddRadicalApplication<Presentation.MainView>(configuration =>
            {
                configuration.EnableSplashScreen(new SplashScreenConfiguration()
                {
                    // simulates some startup work, the splash screen is shown until it completes
                    StartupAsyncWork = serviceProvider => Task.Delay(3000)
                });
            });

            base.OnFrameworkInitializationCompleted();
        }
    }
}
