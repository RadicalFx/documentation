using Autofac.Extensions.DependencyInjection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Hosting;

namespace ThirdPartyContainer
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var host = new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .AddRadicalApplication<Presentation.MainView>()
                .Build();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Startup += async (s, e) => await host.StartAsync();

                desktop.Exit += async (s, e) =>
                {
                    using (host)
                    {
                        await host.StopAsync();
                    }
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
