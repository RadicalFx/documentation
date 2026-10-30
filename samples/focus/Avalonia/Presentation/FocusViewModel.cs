using Radical.Avalonia.Presentation;
namespace FocusSample.Presentation
{
	public class FocusViewModel : AbstractViewModel
	{
		public void MoveFocusToName() 
		{
			MoveFocusTo( nameof( Name ) );
		}

		public string Name
		{
			get { return this.GetPropertyValue( () => Name ); }
			set { this.SetPropertyValue( () => Name, value ); }
		}
	}
}
