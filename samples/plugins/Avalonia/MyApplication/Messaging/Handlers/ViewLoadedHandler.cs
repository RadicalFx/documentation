using PluginsInfrastructure;
using Radical.ComponentModel.Messaging;
using Radical.Linq;
using Radical.Messaging;
using Radical.Avalonia.ComponentModel;
using Radical.Avalonia.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public override Task HandleAsync(object sender, ViewLoaded message)
        {
            var regionManager = regionService.GetRegionManager(message.View);
            plugins.ForEach(plugin => plugin.OnMainViewLoaded(regionManager));

            return Task.CompletedTask;
        }

        protected override bool OnShouldHandle(object sender, ViewLoaded message)
        {
            return message.View is Presentation.MainView;
        }
    }
}
