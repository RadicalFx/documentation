using PluginsInfrastructure;
using System;
using Topics.Radical.Windows.Presentation.ComponentModel;

namespace AnotherSamplePlugin.Services
{
    class PluginDefinition : IPluginDefinition
    {
        public IViewResolver ViewResolver { get; set; }
        public IRegionService RegionService { get; set; }

        public string Name => "AnotherSamplePlugin";

        public Action Initialize => () => { /* NOP */ };

        public Action<IRegionManager> OnMainViewLoaded => mainViewRegionManager =>
        {
            var view = ViewResolver.GetView<Presentation.AnotherSamplePluginItemView>();
            var region = mainViewRegionManager.GetRegion("MainViewRegion") as IElementsRegion;

            region?.Add(view);
        };
    }
}
