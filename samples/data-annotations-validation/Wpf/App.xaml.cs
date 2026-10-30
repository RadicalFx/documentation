using System.Windows;

namespace DataAnnotationsValidationSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.RequireValidationSampleView>();
        }
    }
}
