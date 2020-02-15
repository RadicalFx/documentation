using Radical.Linq;
using Radical.Samples.Services;
using Radical.Windows.ComponentModel;
using Radical.Windows.Presentation;
using System.Collections.Generic;
using System.Linq;

namespace Radical.Samples.Presentation
{
	class MainViewModel : AbstractViewModel
	{
		public MainViewModel( SamplesManager samplesManager, IViewResolver viewResolver, IRegionService regionService )
		{
			Categories = samplesManager.Categories;

			Categories
				.SelectMany( c => c.Samples )
				.ForEach( s =>
				{
					s.ViewSampleHandler = sample =>
					{
						SelectedSample = sample;
						regionService.GetKnownRegionManager<MainView>()
							.GetRegion<IContentRegion>( "SampleContentRegion" )
							.Content = viewResolver.GetView( SelectedSample.ViewType );
					};
				} );
		}

		public IEnumerable<SamplesManager.SampleCategory> Categories
		{
			get;
			private set;
		}

		public SamplesManager.SampleItem SelectedSample
		{
			get { return GetPropertyValue( () => SelectedSample ); }
			private set { SetPropertyValue( () => SelectedSample, value ); }
		}
	}
}
