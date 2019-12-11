using System;
using Radical.Validation;

namespace Radical.Samples.Presentation.Memento.ComplexGraph
{
	public class Address
	{
		public Address( Person owner )
		{
			Ensure.That( owner ).Named( () => owner ).IsNotNull();

			Person = owner;
		}

		public Person Person { get; private set; }

		public string Street { get; set; }
		public string Number { get; set; }
		public string City { get; set; }
		public string ZipCode { get; set; }
	}
}
