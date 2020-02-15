using MyApp.Presentation;
using Radical.Windows;
using System.Windows;

namespace MyApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var bootstrapper = new ApplicationBootstrapper<MainView>();
        }
    }
}
