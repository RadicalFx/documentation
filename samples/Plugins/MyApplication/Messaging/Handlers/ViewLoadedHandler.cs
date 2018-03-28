using PluginsInfrastructure;
using System.Collections.Generic;
using System.Linq;
using Topics.Radical.ComponentModel.Messaging;
using Topics.Radical.Linq;
using Topics.Radical.Messaging;
using Topics.Radical.Windows.Presentation.ComponentModel;
using Topics.Radical.Windows.Presentation.Messaging;

namespace MyApplication.Messaging.Handlers
{
    class ViewLoadedHandler : AbstractMessageHandler<ViewLoaded>, INeedSafeSubscription
    {
        IRegionService regionService;
        IEnumerable<IPluginDefinition> plugins;

        public ViewLoadedHandler(IRegionService regionService, IPluginDefinition[] plugins)
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
