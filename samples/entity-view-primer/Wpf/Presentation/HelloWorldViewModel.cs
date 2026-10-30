using Radical.Windows.Presentation;
using Radical.ComponentModel;
using Radical.Model;
using EntityViewPrimerSample.Model;
using System.Collections.Generic;

namespace EntityViewPrimerSample.Presentation
{
	public class HelloWorldViewModel : AbstractViewModel
	{
		public HelloWorldViewModel()
		{
			var data = new List<Person>()
			{
				new Person( "topics" )
				{
					FirstName = "Mauro",
					LastName = "Servienti"
				},

				new Person( "gioffy" )
				{
					FirstName = "Giorgio",
					LastName = "Formica"
				},

				new Person( "imperugo" )
				{
					FirstName = "Ugo",
					LastName = "Lattanzi"
				}
			};

			Items = new EntityView<Person>( data );
			Items.AddingNew += ( s, e ) => 
			{
				e.NewItem = new Person("--empty--");
			};
		}

		public IEntityView<Person> Items { get; private set; }
	}
}