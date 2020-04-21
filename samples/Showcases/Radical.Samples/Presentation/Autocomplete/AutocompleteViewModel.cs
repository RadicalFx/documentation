using Radical.Samples.ComponentModel;

namespace Radical.Samples.Presentation.Autocomplete
{
	[Sample( Title = "Autocomplete", Category = Categories.Presentation )]
	public class AutocompleteViewModel : SampleViewModel
	{
		public Person Choosen
		{
			get { return GetPropertyValue( () => Choosen ); }
			set { SetPropertyValue( () => Choosen, value ); }
		}
	}
}