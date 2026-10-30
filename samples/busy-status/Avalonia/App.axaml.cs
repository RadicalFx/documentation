using Avalonia;
using Avalonia.Markup.Xaml;

namespace BusyStatusSample
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            this.AddRadicalApplication<Presentation.BusyBehaviorSampleView>();

            base.OnFrameworkInitializationCompleted();
        }
    }
}
