using Radical.Windows.ComponentModel;
using System.Windows.Controls;

namespace TabControlRegionSample.Presentation
{
    /// <summary>
    /// Interaction logic for BetaView.xaml
    /// </summary>
    [InjectViewInRegion(Named = "MainTabRegion")]
    public partial class BetaView : UserControl
    {
        public BetaView()
        {
            InitializeComponent();
        }
    }
}
