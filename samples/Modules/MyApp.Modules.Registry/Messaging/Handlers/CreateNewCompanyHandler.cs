using MyApp.Contracts;
using Radical.ComponentModel.Messaging;
using Radical.Messaging;
using Radical.Windows.ComponentModel;

namespace MyApp.Modules.Registry.Messaging.Handlers
{
    class CreateNewCompanyHandler : AbstractMessageHandler<CreateNewCompany>, INeedSafeSubscription
    {
        readonly IViewResolver viewResolver;
        readonly IRegionService regionService;

        public CreateNewCompanyHandler(IRegionService regionService, IViewResolver viewResolver)
        {
            this.viewResolver = viewResolver;
            this.regionService = regionService;
        }

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
