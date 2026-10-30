using Radical.ComponentModel.Messaging;

namespace MyApp.Modules.Registry.Presentation.Partial.CommandsRegion
{
    class DoSomethingCommandViewModel(IMessageBroker broker)
    {
        public void DoWhatever() 
        {
            broker.Broadcast( this, new Contracts.Messaging.SharedMessage() 
            {
                Text = "Hi, there!"
            } );
        }
    }
}
