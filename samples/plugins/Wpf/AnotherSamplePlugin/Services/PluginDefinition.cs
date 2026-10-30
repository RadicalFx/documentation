using PluginsInfrastructure;
using Radical.Windows.ComponentModel;
using System;

namespace AnotherSamplePlugin.Services
{
    class PluginDefinition(IViewResolver viewResolver) : IPluginDefinition
    {
        public string Name => "AnotherSamplePlugin";

        public Action Initialize => () => { /* NOP */ };

        public Action<IRegionManager> OnMainViewLoaded => mainViewRegionManager =>
        {
            var view = viewResolver.GetView<Presentation.AnotherSamplePluginItemView>();
            var region = mainViewRegionManager.GetRegion("MainViewRegion") as IElementsRegion;

            region?.Add(view);
        };
    }
}
