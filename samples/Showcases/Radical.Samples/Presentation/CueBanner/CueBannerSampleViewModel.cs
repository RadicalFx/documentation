using Radical.Samples.ComponentModel;

namespace Radical.Samples.Presentation.CueBanner
{
	[Sample( Title = "CueBanner behavior", Category = Categories.Behaviors )]
	class CueBannerSampleViewModel : SampleViewModel
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
