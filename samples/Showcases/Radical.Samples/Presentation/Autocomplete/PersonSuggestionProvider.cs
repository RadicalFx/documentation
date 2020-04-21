using AutoCompleteTextBox.Editors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radical.Samples.Presentation.Autocomplete
{
    public class PersonSuggestionProvider : ISuggestionProvider
    {
        public IEnumerable GetSuggestions(string filter)
        {
            if (filter?.Length >= 3)
            {
                return storage.Where(p => p.FullName.StartsWith(filter, StringComparison.OrdinalIgnoreCase));
            }
            return default;
        }

        IList<Person> storage = new List<Person>()
        {
            new Person(){ FirstName = "Mauro", LastName = "Servienti" },
            new Person(){ FirstName = "Giorgio", LastName = "Formica" },
            new Person(){ FirstName = "Giorgio", LastName = "Gentili" },
            new Person(){ FirstName = "Giorgio", LastName = "Gerosa" },
            new Person(){ FirstName = "Daniele", LastName = "Restelli" }
        };
    }
}
