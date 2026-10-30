using MyApp.Contracts;
using Radical.ComponentModel.Messaging;
using Radical.Messaging;
using Radical.Windows.ComponentModel;

namespace MyApp.Modules.Registry.Messaging.Handlers
{
    class CreateNewCompanyHandler(IRegionService regionService, IViewResolver viewResolver)
        : AbstractMessageHandler<CreateNewCompany>, INeedSafeSubscription
    {
        public override void Handle( object sender, CreateNewCompany message )
        {
            var manager = regionService.FindRegionManager( ( obj, rm ) => obj is IMainView );

            if ( manager != null )
            {
                var region = manager.GetRegion<IContentRegion>( "MainContent" );
                var view = viewResolver.GetView<Presentation.NewCompanyView>();

                region.Content = view;
            }
        }
    }
}
