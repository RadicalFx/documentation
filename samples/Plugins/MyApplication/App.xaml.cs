using PluginsInfrastructure;
using System.Linq;
using System.Windows;
using Radical.Linq;
using Microsoft.Extensions.DependencyInjection;

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
