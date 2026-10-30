using Radical.Windows.Presentation;
namespace FocusSample.Presentation
{
	public class FocusViewModel : AbstractViewModel
	{
		public void MoveFocusToName() 
		{
			this.MoveFocusTo( nameof( Name ) );
		}

		public string Name
		{
			get { return this.GetPropertyValue( () => Name ); }
			set { this.SetPropertyValue( () => Name, value ); }
		}
	}
}
