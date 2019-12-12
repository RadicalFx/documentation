using System.Windows;
using Radical.Windows.Presentation.Boot;
using Radical.Windows.Presentation.ComponentModel;
using Radical.Samples.Presentation.ViewModelAsStaticResource;

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

                    //TODO: TypedFactoryFacility
                    //var w = (ServiceProviderWrapper)container;
                    //w.Container.AddFacility<TypedFactoryFacility>();
                });
        }
    }
}
