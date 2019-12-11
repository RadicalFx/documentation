using System.Collections.Generic;
using System.Linq;
using Radical.Windows.Presentation;
using Radical.Linq;
using Radical.Windows.Presentation.ComponentModel;
using Radical.Samples;
using Radical.Samples.Services;

namespace Radical.Presentation.Presentation
{
	class MainViewModel : AbstractViewModel
	{
		readonly SamplesManager samplesManager;
		readonly IViewResolver viewResolver;
		readonly IRegionService regionService;

		public MainViewModel( SamplesManager samplesManager, IViewResolver viewResolver, IRegionService regionService )
		{
			this.samplesManager = samplesManager;
			this.viewResolver = viewResolver;
			this.regionService = regionService;

			Categories = samplesManager.Categories;

			Categories
				.SelectMany( c => c.Samples )
				.ForEach( s =>
				{
					s.ViewSampleHandler = sample =>
					{
						SelectedSample = sample;
						this.regionService.GetKnownRegionManager<MainView>()
							.GetRegion<IContentRegion>( "SampleContentRegion" )
							.Content = this.viewResolver.GetView( SelectedSample.ViewType );
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
