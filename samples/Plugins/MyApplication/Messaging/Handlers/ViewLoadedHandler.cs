using PluginsInfrastructure;
using System.Collections.Generic;
using System.Linq;
using Radical.ComponentModel.Messaging;
using Radical.Linq;
using Radical.Messaging;
using Radical.Windows.ComponentModel;
using Radical.Windows.Messaging;

namespace MyApplication.Messaging.Handlers
{
    class ViewLoadedHandler : AbstractMessageHandler<ViewLoaded>, INeedSafeSubscription
    {
        readonly IRegionService regionService;
        readonly IEnumerable<IPluginDefinition> plugins;

        public ViewLoadedHandler(IRegionService regionService, IEnumerable<IPluginDefinition> plugins)
        {
            this.regionService = regionService;
            this.plugins = plugins.OrderBy(plugin=>plugin.Name);
        }

        public override void Handle(object sender, ViewLoaded message)
        {
            var regionManager = regionService.GetRegionManager(message.View);
            plugins.ForEach(plugin => plugin.OnMainViewLoaded(regionManager));
        }

        protected override bool OnShouldHandle(object sender, ViewLoaded message)
        {
            return message.View is Presentation.MainView;
        }
    }
}
