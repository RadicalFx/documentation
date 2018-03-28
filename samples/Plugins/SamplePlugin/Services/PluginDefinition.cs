using PluginsInfrastructure;
using System;
using Topics.Radical.Windows.Presentation.ComponentModel;

namespace SamplePlugin.Services
{
    class PluginDefinition : IPluginDefinition
    {
        public IViewResolver ViewResolver { get; set; }
        public IRegionService RegionService { get; set; }

        public string Name => "SamplePlugin";

        public Action Initialize => () => { /* NOP */ };

        public Action<IRegionManager> OnMainViewLoaded => mainViewRegionManager =>
        {
            var view = ViewResolver.GetView<Presentation.SamplePluginItemView>();
            var region = mainViewRegionManager.GetRegion("MainViewRegion") as IElementsRegion;

            region?.Add(view);
        };
    }
}
