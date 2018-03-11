using Castle;
using PluginsInfrastructure;
using System.Linq;
using System.Windows;
using Topics.Radical.Linq;
using Topics.Radical.Windows.Presentation.Boot;

namespace MyApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>()
                .OnBootCompleted(serviceProvider =>
                {
                    ((ServiceProviderWrapper)serviceProvider)
                        .Container
                        .ResolveAll<IPluginDefinition>()
                        .OrderBy(plugin => plugin.Name)
                        .ForEach(plugin => plugin.Initialize());
                });
        }
    }
}
