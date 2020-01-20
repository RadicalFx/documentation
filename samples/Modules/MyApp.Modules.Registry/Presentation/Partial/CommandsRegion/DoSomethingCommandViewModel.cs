using Radical.ComponentModel.Messaging;

namespace MyApp.Modules.Registry.Presentation.Partial.CommandsRegion
{
    class DoSomethingCommandViewModel
    {
        readonly IMessageBroker broker;

        public DoSomethingCommandViewModel( IMessageBroker broker )
        {
            this.broker = broker;
        }

        public void DoWhatever() 
        {
            this.broker.Broadcast( this, new Contracts.Messaging.SharedMessage() 
            {
                Text = "Hi, there!"
            } );
        }
    }
}
