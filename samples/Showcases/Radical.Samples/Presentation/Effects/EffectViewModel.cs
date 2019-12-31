using Radical.Samples.ComponentModel;

namespace Radical.Presentation.Effects
{
	[Sample( Title = "Grayscale Shader", Category = Categories.Presentation )]
	public class EffectViewModel : SampleViewModel
	{
		public EffectViewModel()
		{
			SetInitialPropertyValue( () => IsChecked, true );
		}

		public bool IsChecked
		{
			get { return GetPropertyValue( () => IsChecked ); }
			set { SetPropertyValue( () => IsChecked, value ); }
		}
	}
}
