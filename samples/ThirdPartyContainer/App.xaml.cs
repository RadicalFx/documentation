using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace ThirdPartyContainer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var host = new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .AddRadicalApplication<Presentation.MainView>()
                .Build();

            Startup += async (s, e) => await host.StartAsync();

            Exit += async (s, e) =>
            {
                using (host)
                {
                    await host?.StopAsync();
                }
            };
        }
    }
}
