using PluginsInfrastructure;
using Radical.Windows.ComponentModel;
using System;

namespace AnotherSamplePlugin.Services
{
    class PluginDefinition : IPluginDefinition
    {
        readonly IViewResolver viewResolver;

        public PluginDefinition(IViewResolver viewResolver)
        {
            this.viewResolver = viewResolver;
        }

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
