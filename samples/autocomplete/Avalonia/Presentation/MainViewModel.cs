using AutocompleteSample.BindingObjects;
using AutocompleteSample.Providers;
using Radical.Avalonia.Presentation;
using System.Collections.Generic;

namespace AutocompleteSample.Presentation
{
    public class MainViewModel : AbstractViewModel
    {
        public IEnumerable<Person> People { get; } = PersonRegistry.List;

        public Person Choosen
        {
            get { return GetPropertyValue(() => Choosen); }
            set { SetPropertyValue(() => Choosen, value); }
        }
    }
}
