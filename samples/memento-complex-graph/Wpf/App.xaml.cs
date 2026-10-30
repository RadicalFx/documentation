using System.Windows;

namespace MementoComplexGraphSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.ComplexGraphView>();
        }
    }
}
