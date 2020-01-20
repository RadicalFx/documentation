using Radical.ComponentModel;
using Radical.ComponentModel.Windows.Input;
using Radical.Model;
using Radical.Samples.ComponentModel;
using Radical.Windows.Input;
using System.Collections.Generic;

namespace Radical.Samples.Presentation.EntityView
{
	[Sample( Title = "EntityView Custom Property Mapping", Category = Categories.IEntityView )]
	public class CustomPropertyMappingViewModel : SampleViewModel
	{
		string propertyName = "";

		public CustomPropertyMappingViewModel()
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

            //var data = new List<dynamic>()
            //{
            //    new
            //    {
            //        Username = "topics",
            //        FirstName = "Mauro",
            //        LastName = "Servienti"
            //    },

            //    new 
            //    {
            //        Username = "gioffy",
            //        FirstName = "Giorgio",
            //        LastName = "Formica"
            //    },

            //    new
            //    {
            //        Username = "imperugo",
            //        FirstName = "Ugo",
            //        LastName = "Lattanzi"
            //    }
            //};

			ToggleCustomProperty = DelegateCommand.Create()
				.OnExecute( o =>
				{
					var temp = Items;
					Items = null;
					this.OnPropertyChanged( () => Items );

					if( !temp.IsCustomPropertyDefined( propertyName ) )
					{
						var prop = temp.AddCustomProperty<string>( "Nome proprietà", obj =>
						{
							return obj.Item.EntityItem.FirstName + " " + obj.Item.EntityItem.LastName;
						} );

						propertyName = prop.Name;
					}
					else
					{
						temp.RemoveCustomProperty( propertyName );
					}

					Items = temp;
					this.OnPropertyChanged( () => Items );
				} );

			Items = new EntityView<Person>( data );
			Items.AddingNew += ( s, e ) =>
			{
				e.NewItem = new Person( "--empty--" );
			};

		}

		public IEntityView<Person> Items { get; private set; }

		public IDelegateCommand ToggleCustomProperty
		{
			get;
			private set;
		}
	}
}