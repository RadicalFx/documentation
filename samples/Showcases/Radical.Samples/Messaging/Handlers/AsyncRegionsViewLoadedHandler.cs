using Radical.ComponentModel.Messaging;
using Radical.Messaging;
using Radical.Samples.Presentation.AsyncRegions;
using Radical.Windows.Presentation.ComponentModel;
using Radical.Windows.Presentation.Messaging;
using Radical.Windows.Presentation.Regions;

namespace Radical.Samples.Messaging.Handlers
{
	class AsyncRegionsViewLoadedHandler : AbstractMessageHandler<ViewLoaded>, INeedSafeSubscription
	{
		readonly IViewResolver viewResolver;
		readonly IRegionService regionService;

		public AsyncRegionsViewLoadedHandler( IViewResolver viewResolver, IRegionService regionService )
		{
			this.viewResolver = viewResolver;
			this.regionService = regionService;
		}

		protected override bool OnShouldHandle(object sender, ViewLoaded message )
		{
			return message.View is AsyncRegionsView;
		}

		public override void Handle(object sender, ViewLoaded message )
		{
			if ( regionService.HoldsRegionManager( message.View ) )
			{
				regionService.GetRegionManager( message.View )
					.GetRegion<IContentRegion>( "AsyncFoo" )
					.SetContentAsync( () => 
					{
						var foo = viewResolver.GetView<FooView>();
						return foo;
					} );
			}
		}
	}
}