﻿using Radical.ComponentModel;
using Radical.Model;
using Radical.Samples.ComponentModel;
using System.Collections.Generic;

namespace Radical.Samples.Presentation.EntityView
{
	[Sample( Title = "EntityView Primer", Category = Categories.IEntityView )]
	public class HelloWorldViewModel : SampleViewModel
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