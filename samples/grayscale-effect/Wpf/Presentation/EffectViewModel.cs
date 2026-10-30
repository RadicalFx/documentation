using Radical.Windows.Presentation;
namespace GrayscaleEffectSample.Presentation
{
	public class EffectViewModel : AbstractViewModel
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
