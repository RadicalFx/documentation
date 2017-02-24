using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Radical.ComponentModel.Messaging;

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
            this.broker.Broadcast( this, new MyApp.Contracts.Messaging.SharedMessage() 
            {
                Text = "Hi, there!"
            } );
        }
    }
}
