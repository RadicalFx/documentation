using Radical.ComponentModel.Messaging;
using System.Threading.Tasks;

namespace MyApp.Modules.Registry.Presentation.Partial.CommandsRegion
{
    class DoSomethingCommandViewModel(IMessageBroker broker)
    {
        public Task DoWhatever() 
        {
            return broker.BroadcastAsync( this, new Contracts.Messaging.SharedMessage() 
            {
                Text = "Hi, there!"
            } );
        }
    }
}
