using MyApp.Contracts;
using System.Windows;

namespace MyApp.Presentation
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window, IMainView
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}
