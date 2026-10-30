using System.Windows;

namespace EntityViewCustomPropertyMappingSample
{
    public partial class App : Application
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.CustomPropertyMappingView>();
        }
    }
}
