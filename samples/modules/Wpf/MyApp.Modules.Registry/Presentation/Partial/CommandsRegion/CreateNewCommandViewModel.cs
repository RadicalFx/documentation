using MyApp.Contracts.ComponentModel;
using Radical.ComponentModel.Messaging;
using Radical.Windows.Presentation;
using System.Threading.Tasks;

namespace MyApp.Modules.Registry.Presentation.Partial.CommandsRegion
{
    class CreateNewCommandViewModel(IMessageBroker broker, ISampleSharedService _) : AbstractViewModel
    {
        public Task Create() 
        {
            return broker.BroadcastAsync( this, new Messaging.CreateNewCompany() );
        }
    }
}
