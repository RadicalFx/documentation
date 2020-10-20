using Radical.Windows.Presentation;

namespace BasicSample.Presentation
{
    public class MainViewModel : AbstractViewModel
    {
        public MainViewModel()
        {
            var propertyMetadata = GetPropertyMetadata(() => Text);
            propertyMetadata.DefaultValue = "Text property default value";
        }

        public string Text
        {
            get => GetPropertyValue(()=>Text);
            set => SetPropertyValue(()=> Text, value);
        }
    }
}
