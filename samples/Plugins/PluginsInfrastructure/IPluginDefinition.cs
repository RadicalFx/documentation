using System;
using Radical.Windows.ComponentModel;

namespace PluginsInfrastructure
{
    public interface IPluginDefinition
    {
        string Name { get; }
        Action Initialize { get; }
        Action<IRegionManager> OnMainViewLoaded { get; }
    }
}
