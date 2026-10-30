using Microsoft.Extensions.DependencyInjection;
using Radical.Windows.ComponentModel;
using System.Windows;

namespace ViewModelAsResourceSample
{
    public partial class App : Application
    {
        public App()
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
        }
    }
}
