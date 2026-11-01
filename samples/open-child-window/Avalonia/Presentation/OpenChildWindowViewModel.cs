using Radical.Avalonia.Presentation;
using Radical.ComponentModel.Messaging;
using System.Threading.Tasks;

namespace OpenChildWindowSample.Presentation
{
	public class OpenChildWindowViewModel : AbstractViewModel
	{
	    readonly IMessageBroker broker;

        public OpenChildWindowViewModel( IMessageBroker broker )
        {
            this.broker = broker;
        }

	    public Task OpenWindow()
	    {
	        return broker.BroadcastAsync(this, new Messaging.OpenChildWindowSampleMessage()
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
