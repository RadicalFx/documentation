using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MyApp.Contracts;
using Topics.Radical.ComponentModel.Messaging;
using Topics.Radical.Messaging;
using Topics.Radical.Windows.Input;
using Topics.Radical.Windows.Presentation.ComponentModel;
using Topics.Radical.Windows.Presentation.Messaging;

namespace MyApp.Modules.Registry.Messaging.Handlers
{
    class CreateNewCompanyHandler : AbstractMessageHandler<CreateNewCompany>, INeedSafeSubscription
    {
        public IConventionsHandler Conventions { get; set; }

        public IViewResolver ViewResolver { get; set; }

        public IRegionService RegionService { get; set; }

        public override void Handle( object sender, CreateNewCompany message )
        {
            var manager = this.RegionService.FindRegionManager( ( obj, rm ) => obj is IMainView );

            if ( manager != null )
            {
                var region = manager.GetRegion<IContentRegion>( "MainContent" );
                var view = this.ViewResolver.GetView<Presentation.NewCompanyView>();

                region.Content = view;
            }
        }
    }
}
