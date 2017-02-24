using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Topics.Radical.ComponentModel.Messaging;
using Topics.Radical.Messaging;
using Topics.Radical.Windows.Input;
using Topics.Radical.Windows.Presentation.ComponentModel;
using Topics.Radical.Windows.Presentation.Messaging;

namespace MyApp.Modules.Registry.Messaging.Handlers
{
    class MainViewLoadedHandler : AbstractMessageHandler<ViewLoaded>, INeedSafeSubscription
    {
        public IConventionsHandler Conventions { get; set; }

        public IViewResolver ViewResolver { get; set; }

        public IRegionService RegionService { get; set; }

        public IMessageBroker Broker { get; set; }

        protected override bool OnShouldHandle( object sender, ViewLoaded message )
        {
            var viewModel = this.Conventions.GetViewDataContext( message.View, ViewDataContextSearchBehavior.LocalOnly );
            return viewModel is Contracts.IMainViewModel;
        }

        public override void Handle( object sender, ViewLoaded message )
        {
            if ( this.RegionService.HoldsRegionManager( message.View ) )
            {
                var manager = this.RegionService.GetRegionManager( message.View );

                var registryMenu = new MenuItem();
                registryMenu.Header = "Registry";
                registryMenu.Items.Add( new MenuItem()
                {
                    Header = "Create...",
                    Command = DelegateCommand.Create()
                                .OnExecute( o =>
                                {
                                    this.Broker.Broadcast( this, new Messaging.CreateNewCompany() );
                                } )
                } );

                manager.GetRegion<IElementsRegion>( "MainMenuRegion" ).Add( registryMenu );
            }
        }
    }
}
