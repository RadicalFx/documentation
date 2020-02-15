using Radical.Samples.Presentation.ViewModelAsStaticResource;
using Radical.Windows;
using Radical.Windows.ComponentModel;
using System.Windows;

namespace Radical.Samples
{
    public partial class App : Application
    {
        public App()
        {
            var bootstrapper = new ApplicationBootstrapper<Presentation.MainView>()
                .EnableSplashScreen()
                .OnBoot(container =>
                {
                   var conventions = container.GetService<IConventionsHandler>();
                   /*
                    * the following will make every ViewModel 
                    * appear as a resource in bound Views
                    * --> conventions.ShouldExposeViewModelAsStaticResource = (view, viewModel) => true;
                    */
                   conventions.ShouldExposeViewModelAsStaticResource = (view, viewModel) =>
                   {
                       return (viewModel.GetType() == typeof(SampleViewModelAsResourceViewModel))
                        ? true
                        : conventions.DefaultShouldExposeViewModelAsStaticResource(view, viewModel);
                   };
                });
        }
    }
}
