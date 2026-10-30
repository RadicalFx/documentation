using System.Windows;

namespace GrayscaleEffectSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.EffectView>();
        }
    }
}
