using Avalonia;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Radical.Avalonia.ComponentModel;

namespace ViewModelAsResourceSample
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            this.AddRadicalApplication<Presentation.SampleViewModelAsResourceView>(configuration =>
            {
                configuration.OnBooting(container =>
                {
                    var conventions = container.GetService<IConventionsHandler>();
                    /*
                     * the following will make every ViewModel
                     * appear as a resource in bound Views
                     * --> conventions.ShouldExposeViewModelAsStaticResource = (view, viewModel) => true;
                     */
                    conventions.ShouldExposeViewModelAsStaticResource = (view, viewModel) =>
                    {
                        return (viewModel.GetType() == typeof(Presentation.SampleViewModelAsResourceViewModel))
                         ? true
                         : conventions.DefaultShouldExposeViewModelAsStaticResource(view, viewModel);
                    };
                });
            });

            base.OnFrameworkInitializationCompleted();
        }
    }
}
