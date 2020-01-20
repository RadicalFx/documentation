using Radical.ComponentModel;
using Radical.ComponentModel.ChangeTracking;
using Radical.Model;

namespace Radical.Samples.Presentation.Memento.ComplexGraph
{
	public class PersonViewModel : MementoEntity
	{
		MementoEntityCollection<AddressViewModel> addressesDataSource;

		public void Initialize( Person person, bool registerAsTransient )
		{
			if( registerAsTransient )
			{
				RegisterTransient();
			}

			SetInitialPropertyValue( () => FirstName, person.FirstName );
			SetInitialPropertyValue( () => LastName, person.LastName );

			addressesDataSource = new MementoEntityCollection<AddressViewModel>();
			addressesDataSource.BulkLoad( person.Addresses, a =>
				{
					var vm = CreateAddressViewModel( a, registerAsTransient );
					return vm;
				} );

			Addresses = addressesDataSource.DefaultView;
			Addresses.AddingNew += ( s, e ) =>
			{
				var vm = CreateAddressViewModel( null, true );

				e.NewItem = vm;
				e.AutoCommit = true;
			};
		}

		AddressViewModel CreateAddressViewModel( Address a, bool registerAsTransient )
		{
			var vm = new AddressViewModel();

			vm.Initialize( a, registerAsTransient );

			return vm;
		}

		protected override void OnMementoChanged( IChangeTrackingService newMemento, IChangeTrackingService oldMemento )
		{
			base.OnMementoChanged( newMemento, oldMemento );

			if( oldMemento != null )
			{
				oldMemento.Detach( addressesDataSource );
			}

			if( newMemento != null )
			{
				newMemento.Attach( addressesDataSource );
			}
		}

		public string FirstName
		{
			get { return GetPropertyValue( () => FirstName ); }
			set { SetPropertyValue( () => FirstName, value ); }
		}

		public string LastName
		{
			get { return GetPropertyValue( () => LastName ); }
			set { SetPropertyValue( () => LastName, value ); }
		}

		public IEntityView<AddressViewModel> Addresses
		{
			get;
			private set;
		}
	}
}
