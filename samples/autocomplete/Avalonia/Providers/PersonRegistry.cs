using AutocompleteSample.BindingObjects;
using System.Collections.Generic;

namespace AutocompleteSample.Providers
{
    public static class PersonRegistry
    {
        public static IEnumerable<Person> List
        {
            get
            {
                return new[]
                {
                    new Person{ FirstName = "Peter", LastName = "Parker", Superhero = "Spider-Man" },
                    new Person{ FirstName = "Bruce", LastName = "Wayne", Superhero = "Batman" },
                    new Person{ FirstName = "Bruce", LastName = "Banner", Superhero="Incredible Hulk" },
                    new Person{ FirstName = "Britt", LastName = "Reid", Superhero="Green Hornet"  },
                    new Person{ FirstName = "Steve", LastName = "Rogers", Superhero="Captain America" },
                    new Person{ FirstName = "Stanley", LastName = "Beamish", Superhero="Mr. Terrific" },
                    new Person{ FirstName = "Clark", LastName = "Kent", Superhero="Superman" }
                };
            }
        }

    }
}
