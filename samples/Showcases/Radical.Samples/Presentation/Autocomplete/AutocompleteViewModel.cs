using System;
using System.Linq;
using Radical.ComponentModel;
using Radical.Observers;
using Radical.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using Radical.Samples.ComponentModel;

namespace Radical.Samples.Presentation.Autocomplete
{
	[Sample( Title = "Autocomplete", Category = Categories.Presentation )]
	public class AutocompleteViewModel : SampleViewModel
	{
		IList<Person> storage = new List<Person>() 
		{
			new Person(){ FirstName = "Mauro", LastName = "Servienti" },
			new Person(){ FirstName = "Giorgio", LastName = "Formica" },
			new Person(){ FirstName = "Giorgio", LastName = "Gentili" },
			new Person(){ FirstName = "Giorgio", LastName = "Gerosa" },
			new Person(){ FirstName = "Daniele", LastName = "Restelli" }
		};

		public AutocompleteViewModel()
		{
			Data = new ObservableCollection<Person>();

			PropertyObserver.For( this )
				.Observe( v => v.Choosen, ( v, s ) =>
				{
					var value = v.Choosen;
				} )
				.Observe( v => v.UserText, ( v, s ) =>
				{
					if( UserText.Length >= 3 )
				    {
				        Data.Clear();
						var filtered = storage.Where( p => p.FullName.StartsWith( UserText, StringComparison.OrdinalIgnoreCase ) );
						foreach( var item in filtered )
						{
							Data.Add( item );
						}
				    }
				} );
		}

		public string UserText
		{
			get { return GetPropertyValue( () => UserText ); }
			set { SetPropertyValue( () => UserText, value ); }
		}

		public ObservableCollection<Person> Data { get; private set; }

		public Person Choosen
		{
			get { return GetPropertyValue( () => Choosen ); }
			set { SetPropertyValue( () => Choosen, value ); }
		}
	}
}