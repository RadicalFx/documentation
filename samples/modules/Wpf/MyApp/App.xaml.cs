using MyApp.Presentation;
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
            this.AddRadicalApplication<MainView>();
        }
    }
}
