using System.Windows;

namespace DragAndDropSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.DragAndDropSampleView>();
        }
    }
}
