using System.Windows;

namespace BusyStatusSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.BusyBehaviorSampleView>();
        }
    }
}
