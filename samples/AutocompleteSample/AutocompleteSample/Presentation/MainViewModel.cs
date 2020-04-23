using AutocompleteSample.BindingObjects;
using Radical.Windows.Presentation;

namespace AutocompleteSample.Presentation
{
    public class MainViewModel : AbstractViewModel
    {    
        public Person Choosen
        {
            get { return GetPropertyValue(() => Choosen); }
            set { SetPropertyValue(() => Choosen, value); }
        }
    }
}
