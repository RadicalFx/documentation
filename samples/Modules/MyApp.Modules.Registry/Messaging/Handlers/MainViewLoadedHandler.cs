using Radical.ComponentModel.Messaging;
using Radical.Messaging;
using Radical.Windows.Input;
using Radical.Windows.ComponentModel;
using Radical.Windows.Messaging;
using System.Windows.Controls;

namespace MyApp.Modules.Registry.Messaging.Handlers
{
    class MainViewLoadedHandler : AbstractMessageHandler<ViewLoaded>, INeedSafeSubscription
    {
        readonly IConventionsHandler conventions;
        readonly IMessageBroker broker;
        readonly IRegionService regionService;

        public MainViewLoadedHandler(IConventionsHandler conventions, IRegionService regionService, IMessageBroker broker)
        {
            this.conventions = conventions;
            this.regionService = regionService;
            this.broker = broker;
        }

        protected override bool OnShouldHandle( object sender, ViewLoaded message )
        {
            var viewModel = conventions.GetViewDataContext( message.View, ViewDataContextSearchBehavior.LocalOnly );
            return viewModel is Contracts.IMainViewModel;
        }

        public override void Handle( object sender, ViewLoaded message )
        {
            if ( regionService.HoldsRegionManager( message.View ) )
            {
                var manager = regionService.GetRegionManager( message.View );

                var registryMenu = new MenuItem
                {
                    Header = "Registry"
                };
                registryMenu.Items.Add( new MenuItem()
                {
                    Header = "Create...",
                    Command = DelegateCommand.Create()
                                .OnExecute( o =>
                                {
                                    broker.Broadcast( this, new CreateNewCompany() );
                                } )
                } );

                manager.GetRegion<IElementsRegion>( "MainMenuRegion" ).Add( registryMenu );
            }
        }
    }
}
