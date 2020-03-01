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
            var bootstrapper = new ApplicationBootstrapper<Presentation.MainView>()
                .OnBootCompleted(serviceProvider =>
                {
                    serviceProvider.GetServices<IPluginDefinition>()
                        .OrderBy(plugin => plugin.Name)
                        .ForEach(plugin => plugin.Initialize());
                });
        }
    }
}
