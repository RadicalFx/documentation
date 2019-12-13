using Radical.Model;

namespace Radical.Samples.Presentation.Memento.ComplexGraph
{
	public class AddressViewModel : MementoEntity
	{
		public void Initialize( Address address, bool registerAsTransient )
		{
			if( registerAsTransient )
			{
				RegisterTransient();
			}

			SetInitialPropertyValue
			(
				() => Street,
				address.With( a => a.Street ).Return( s => s, "" )
			);

			SetInitialPropertyValue
			(
				() => Number,
				address.With( a => a.Number ).Return( n => n, "" )
			);

			SetInitialPropertyValue
			(
				() => City,
				address.With( a => a.City ).Return( c => c, "" )
			);

			SetInitialPropertyValue
			(
				() => ZipCode,
				address.With( a => a.ZipCode ).Return( zc => zc, "" )
			);
		}

		public string Street
		{
			get { return GetPropertyValue( () => Street ); }
			set { SetPropertyValue( () => Street, value ); }
		}

		public string Number
		{
			get { return GetPropertyValue( () => Number ); }
			set { SetPropertyValue( () => Number, value ); }
		}

		public string City
		{
			get { return GetPropertyValue( () => City ); }
			set { SetPropertyValue( () => City, value ); }
		}

		public string ZipCode
		{
			get { return GetPropertyValue( () => ZipCode ); }
			set { SetPropertyValue( () => ZipCode, value ); }
		}
	}
}
