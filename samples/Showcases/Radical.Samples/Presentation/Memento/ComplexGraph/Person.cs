using System.Collections.Generic;

namespace Radical.Samples.Presentation.Memento.ComplexGraph
{
	public class Person
	{
		public Person()
		{
			Addresses = new List<Address>();
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }

		public IList<Address> Addresses { get; private set; }
	}
}
