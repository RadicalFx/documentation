using Radical.Windows.Presentation;
using Radical.ComponentModel.Messaging;

namespace OpenChildWindowSample.Presentation
{
	public class OpenChildWindowViewModel : AbstractViewModel
	{
	    readonly IMessageBroker broker;

        public OpenChildWindowViewModel( IMessageBroker broker )
        {
            this.broker = broker;
        }

	    public void OpenWindow()
	    {
	        broker.Broadcast(this, new Messaging.OpenChildWindowSampleMessage()
	        {
	            AsDialog = AsDialog
	        });
	    }

	    public bool AsDialog
		{
			get { return this.GetPropertyValue( () => AsDialog ); }
			set { this.SetPropertyValue( () => AsDialog, value ); }
		}
	}
}
