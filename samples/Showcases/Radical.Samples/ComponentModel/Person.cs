using System.Collections.Generic;
using System.ComponentModel;

namespace Radical.Samples.ComponentModel
{
	public class Person
	{
		public Person()
			: this(string.Empty )
		{

		}

		public Person(string username )
		{
			Username = username;
			Addresses = new List<Address>();
		}

		public string Username { get; private set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }

		[Bindable( BindableSupport.No )]
		public IList<Address> Addresses { get; private set; }
	}
}
