using Microsoft.Extensions.DependencyInjection;
using PluginsInfrastructure;
using Radical.Linq;
using Radical.Windows;
using System.Linq;
using System.Windows;

namespace MyApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
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
        }
    }
}
