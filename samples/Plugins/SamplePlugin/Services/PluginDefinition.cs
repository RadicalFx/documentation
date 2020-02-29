using PluginsInfrastructure;
using Radical.Windows.ComponentModel;
using System;

namespace SamplePlugin.Services
{
    class PluginDefinition : IPluginDefinition
    {
        readonly IViewResolver viewResolver;

        public PluginDefinition(IViewResolver viewResolver)
        {
            this.viewResolver = viewResolver;
        }

        public string Name => "SamplePlugin";

        public Action Initialize => () => { /* NOP */ };

        public Action<IRegionManager> OnMainViewLoaded => mainViewRegionManager =>
        {
            var view = viewResolver.GetView<Presentation.SamplePluginItemView>();
            var region = mainViewRegionManager.GetRegion("MainViewRegion") as IElementsRegion;

            region?.Add(view);
        };
    }
}
