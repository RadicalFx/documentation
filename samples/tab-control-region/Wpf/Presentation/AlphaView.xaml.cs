using Radical.Windows.ComponentModel;
using System.Windows.Controls;

namespace TabControlRegionSample.Presentation
{
    /// <summary>
    /// Interaction logic for AlphaView.xaml
    /// </summary>
    [InjectViewInRegion(Named = "MainTabRegion")]
    public partial class AlphaView : UserControl
    {
        public AlphaView()
        {
            InitializeComponent();
        }
    }
}
