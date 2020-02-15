using Radical.Windows.ComponentModel;
using System.Windows.Controls;

namespace Radical.Samples.Presentation.TabControlRegion
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
