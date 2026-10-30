using Radical.Windows.ComponentModel;
using System;

namespace PluginsInfrastructure
{
    public interface IPluginDefinition
    {
        string Name { get; }
        Action Initialize { get; }
        Action<IRegionManager> OnMainViewLoaded { get; }
    }
}
