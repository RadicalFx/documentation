using Avalonia;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using PluginsInfrastructure;
using Radical.Linq;
using System.Linq;

namespace MyApplication
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            this.AddRadicalApplication<Presentation.MainView>(config =>
            {
                config.OnBootCompleted(serviceProvider =>
                {
                    serviceProvider.GetServices<IPluginDefinition>()
                        .OrderBy(plugin => plugin.Name)
                        .ForEach(plugin => plugin.Initialize());
                });
            });

            base.OnFrameworkInitializationCompleted();
        }
    }
}
