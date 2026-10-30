using MyApp.Contracts.ComponentModel;
using Radical.ComponentModel.Messaging;
using Radical.Avalonia.Presentation;

namespace MyApp.Modules.Registry.Presentation.Partial.CommandsRegion
{
    class CreateNewCommandViewModel(IMessageBroker broker, ISampleSharedService _) : AbstractViewModel
    {
        public void Create() 
        {
            broker.Broadcast( this, new Messaging.CreateNewCompany() );
        }
    }
}
