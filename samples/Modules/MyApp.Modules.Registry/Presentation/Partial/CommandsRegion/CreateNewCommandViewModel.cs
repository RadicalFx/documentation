using MyApp.Contracts.ComponentModel;
using Radical.ComponentModel.Messaging;
using Radical.Windows.Presentation;

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
