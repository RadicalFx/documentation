using Radical.ComponentModel.Messaging;
using Radical.Samples.ComponentModel;

namespace Radical.Samples.Presentation.OpenChildWindow
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
