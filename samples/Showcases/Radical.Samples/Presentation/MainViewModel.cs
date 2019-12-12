﻿using System.Collections.Generic;
using System.Linq;
using Radical.Windows.Presentation;
using Radical.Linq;
using Radical.Windows.Presentation.ComponentModel;
using Radical.Samples.Services;

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
