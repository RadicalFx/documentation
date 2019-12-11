using Radical.ComponentModel;
using Radical.ComponentModel.ChangeTracking;
using Radical.ChangeTracking;
using Radical.Observers;
using Radical.Windows.Input;
using System.Windows.Input;
using Radical.Samples.ComponentModel;

namespace Radical.Samples.Presentation.Memento.SimpleGraph
{
	[Sample( Title = "Simple Graph", Category = Categories.Memento )]
	public class SimpleGraphViewModel : SampleViewModel
	{
		readonly IChangeTrackingService service = new ChangeTrackingService();

		public SimpleGraphViewModel()
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

			var person = new Person()
			{
				FirstName = "Mauro",
				LastName = "Servienti"
			};

			var entity = new PersonViewModel();

			service.Attach( entity );

			entity.Initialize( person, false );

			Entity = entity;
		}

		private PersonViewModel _entity = null;
		public PersonViewModel Entity
		{
			get { return _entity; }
			private set
			{
				if( value != Entity )
				{
					_entity = value;
					this.OnPropertyChanged( () => Entity );
				}
			}
		}

		public ICommand UndoCommand { get; private set; }

		public ICommand RedoCommand { get; private set; }
	}
}