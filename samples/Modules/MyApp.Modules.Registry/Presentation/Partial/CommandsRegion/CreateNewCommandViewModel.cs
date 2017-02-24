using MyApp.Contracts.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Radical.ComponentModel.Messaging;
using Topics.Radical.Windows.Presentation;

namespace MyApp.Modules.Registry.Presentation.Partial.CommandsRegion
{
    class CreateNewCommandViewModel : AbstractViewModel
    {
        readonly IMessageBroker broker;

        public CreateNewCommandViewModel( IMessageBroker broker, ISampleSharedService service)
        {
            this.broker = broker;
        }

        public void Create() 
        {
            this.broker.Broadcast( this, new Messaging.CreateNewCompany() );
        }
    }
}
