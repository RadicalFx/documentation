using Avalonia;
using Avalonia.Markup.Xaml;

namespace AsyncRegionsSample
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            this.AddRadicalApplication<Presentation.AsyncRegionsView>();

            base.OnFrameworkInitializationCompleted();
        }
    }
}
