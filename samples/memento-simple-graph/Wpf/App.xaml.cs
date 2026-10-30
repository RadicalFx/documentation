using System.Windows;

namespace MementoSimpleGraphSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.SimpleGraphView>();
        }
    }
}
