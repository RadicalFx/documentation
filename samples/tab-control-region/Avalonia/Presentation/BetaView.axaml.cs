using Avalonia.Controls;
using Radical.Avalonia.ComponentModel;

namespace TabControlRegionSample.Presentation
{
    [InjectViewInRegion(Named = "MainTabRegion")]
    public partial class BetaView : UserControl
    {
        public BetaView()
        {
            InitializeComponent();
        }
    }
}
