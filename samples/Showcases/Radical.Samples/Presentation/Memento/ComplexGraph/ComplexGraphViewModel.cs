using Radical.ChangeTracking;
using Radical.ComponentModel;
using Radical.ComponentModel.ChangeTracking;
using Radical.Observers;
using Radical.Samples.ComponentModel;
using Radical.Windows.Input;
using System.Linq;
using System.Windows.Input;

namespace Radical.Samples.Presentation.Memento.ComplexGraph
{
	[Sample( Title = "Complex Graph", Category = Categories.Memento )]
	public sealed class ComplexGraphViewModel : SampleViewModel
	{
		readonly IChangeTrackingService service = new ChangeTrackingService();

		public ComplexGraphViewModel()
		{
			var observer = MementoObserver.Monitor( service );

			UndoCommand = DelegateCommand.Create()
				.OnCanExecute( o => service.CanUndo )
				.OnExecute( o => service.Undo() )
				.AddMonitor( observer );

			RedoCommand = DelegateCommand.Create()
				.OnCanExecute( o => service.CanRedo )
				.OnExecute( o => service.Redo() )
				.AddMonitor( observer );

			CreateNewAddressCommand = DelegateCommand.Create()
				.OnExecute( o =>
				{
					var address = Entity.Addresses.AddNew();
					SelectedAddress = address;
				} );

			DeleteAddressCommand = DelegateCommand.Create()
				.OnCanExecute( o => SelectedAddress != null )
				.OnExecute( o =>
				{
					SelectedAddress.Delete();
					SelectedAddress = Entity.Addresses.FirstOrDefault();
				} )
				.AddMonitor
				(
					PropertyObserver.For( this )
						.Observe( v => v.SelectedAddress )
				);

			var person = new Person()
			{
				FirstName = "Mauro",
				LastName = "Servienti"
			};

			person.Addresses.Add( new Address( person )
			{
				City = "Treviglio",
				Number = "11",
				Street = "Where I live",
				ZipCode = "20100"
			} );

			person.Addresses.Add( new Address( person )
			{
				City = "Daolasa",
				Number = "2",
				Street = "Pierino",
				ZipCode = "20100"
			} );

			var entity = new PersonViewModel();
			entity.Initialize( person, false );

			service.Attach( entity );

			Entity = entity;
		}

		public ICommand UndoCommand { get; private set; }

		public ICommand RedoCommand { get; private set; }

		public ICommand CreateNewAddressCommand { get; private set; }

		public ICommand DeleteAddressCommand { get; private set; }

		public PersonViewModel Entity
		{
			get { return GetPropertyValue( () => Entity ); }
			private set { SetPropertyValue( () => Entity, value ); }
		}

		public IEntityItemView<AddressViewModel> SelectedAddress
		{
			get { return GetPropertyValue( () => SelectedAddress ); }
			private set { SetPropertyValue( () => SelectedAddress, value ); }
		}
	}
}
