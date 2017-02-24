using System.Windows;
using Castle;
using Castle.Facilities.TypedFactory;
using Topics.Radical.Windows.Presentation.Boot;
using Topics.Radical.Windows.Presentation.ComponentModel;
using Topics.Radical.Presentation.ViewModelAsStaticResource;

namespace Topics.Radical
{
    public partial class App : Application
    {
        public App()
        {
            var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>()
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

                   var w = (ServiceProviderWrapper)container;
                   w.Container.AddFacility<TypedFactoryFacility>();
               });
        }
    }
}
