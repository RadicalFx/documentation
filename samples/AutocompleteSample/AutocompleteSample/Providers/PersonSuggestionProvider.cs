using AutocompleteSample.BindingObjects;
using AutoCompleteTextBox.Editors;
using System;
using System.Collections;
using System.Linq;

namespace AutocompleteSample.Providers
{
    public class PersonSuggestionProvider : ISuggestionProvider
    {
        public IEnumerable GetSuggestions(string filter)
        {
            return filter.Length >= 3 ? storage.Where(p => p.FullName.StartsWith(filter, StringComparison.OrdinalIgnoreCase)) : default;
        }

        Person[] storage = new []
        {
            new Person(){ FirstName = "Mauro", LastName = "Servienti" },
            new Person(){ FirstName = "Giorgio", LastName = "Formica" },
            new Person(){ FirstName = "Giorgio", LastName = "Gentili" },
            new Person(){ FirstName = "Giorgio", LastName = "Gerosa" },
            new Person(){ FirstName = "Daniele", LastName = "Restelli" }
        };
    }
}
