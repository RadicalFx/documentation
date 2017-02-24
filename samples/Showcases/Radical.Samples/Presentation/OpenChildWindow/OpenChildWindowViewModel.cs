using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topics.Radical.ComponentModel;
using Topics.Radical.ComponentModel.Messaging;

namespace Topics.Radical.Presentation.OpenChildWindow
{
	[Sample( Title = "Open Child Window", Category = Categories.Presentation )]
	public class OpenChildWindowViewModel : SampleViewModel
	{
	    readonly IMessageBroker broker;

        public OpenChildWindowViewModel( IMessageBroker broker )
        {
            this.broker = broker;
        }

	    public void OpenWindow()
	    {
	        this.broker.Broadcast(this, new Messaging.OpenChildWindowSampleMessage()
	        {
	            AsDialog = this.AsDialog
	        });
	    }

	    public Boolean AsDialog
		{
			get { return this.GetPropertyValue( () => this.AsDialog ); }
			set { this.SetPropertyValue( () => this.AsDialog, value ); }
		}
	}
}
