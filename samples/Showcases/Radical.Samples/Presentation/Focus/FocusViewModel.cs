using Radical.Samples.ComponentModel;

namespace Radical.Samples.Presentation.Focus
{
	[Sample( Title = "Focus behavior", Category = Categories.Behaviors )]
	public class FocusViewModel : SampleViewModel
	{
		public void MoveFocusToName() 
		{
			this.MoveFocusTo( () => Name );
		}

		public string Name
		{
			get { return this.GetPropertyValue( () => Name ); }
			set { this.SetPropertyValue( () => Name, value ); }
		}
	}
}
