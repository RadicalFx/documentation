using Radical.ComponentModel;
using System.Collections.Generic;
using Radical.Samples.ComponentModel;

namespace Radical.Samples.Presentation.EntityCollectionView
{
	//[Sample( Title = "Entity CollectionView", Category = Categories.IEntityView )]
	public class EntityCollectionViewViewModel : SampleViewModel
	{
		public EntityCollectionViewViewModel()
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

			//this.Items = new EntityCollectionView<Person>( data );
		}

		public IEntityView Items { get; private set; }
	}
}