using Radical.Windows.Presentation;
namespace CueBannerSample.Presentation
{
	class CueBannerSampleViewModel : AbstractViewModel
	{
		public bool IsPasswordBoxVisible
		{
			get { return this.GetPropertyValue( () => IsPasswordBoxVisible ); }
			set { this.SetPropertyValue( () => IsPasswordBoxVisible, value ); }
		}

		public bool IsTextBoxVisible
		{
			get { return this.GetPropertyValue( () => IsTextBoxVisible ); }
			set { this.SetPropertyValue( () => IsTextBoxVisible, value ); }
		}
	}
}
