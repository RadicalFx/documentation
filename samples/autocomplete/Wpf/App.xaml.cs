using System.Windows;

namespace AutocompleteSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.MainView>();
        }
    }
}
