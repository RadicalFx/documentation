using System;
using Radical.ComponentModel;
using Radical.ComponentModel.ChangeTracking;
using Radical.Model;

namespace Radical.Samples.Presentation.Memento.SimpleGraph
{
	public class PersonViewModel : MementoEntity
	{
		public void Initialize( Person person, bool registerAsTransient )
		{
			if( registerAsTransient )
			{
				RegisterTransient();
			}

			SetInitialPropertyValue( () => FirstName, person.FirstName );
			SetInitialPropertyValue( () => LastName, person.LastName );



			//var bookmark = ( ( IMemento )this ).Memento.CreateBookmark();


			//( ( IMemento )this ).Memento.Revert( bookmark );

			//( ( IMemento )this ).Memento.AcceptChanges();

			//using ( var op = ( ( IMemento )this ).Memento.BeginAtomicOperation() ) 
			//{
			//	this.FirstName = "Mauro";
			//	this.LastName = "Servienti";
			//}

			//( ( IMemento )this ).Memento.Undo();

			//this.Addresses = new MementoEntityCollection<AddressViewModel>();
			//this.Addresses.AddingNew += ( s, e ) =>
			//{
			//    var vm = this.entityViewModelFactory.Create<IPublicationViewModel>();

			//    vm.ParentViewModel = this;
			//    vm.Parent = this.SourceEntity;

			//    vm.Initialize( null, true );

			//    e.NewItem = vm;
			//    e.AutoCommit = true;
			//};

			//if( sourceEntity == null )
			//{
			//    this.SetInitialPropertyValue( () => this.Year, DateTime.Now.Year );
			//    this.SetInitialPropertyValue( () => this.CalendarName, "Calendario pubblicazioni" );

			//    //Qui dobbiamo anche generare le uscite di default
			//    this.InitializeCalendar();
			//}
			//else
			//{
			//    this.SetInitialPropertyValue( () => this.Year, sourceEntity.Year );
			//    this.SetInitialPropertyValue( () => this.CalendarName, sourceEntity.CalendarName );

			//    this.Publications.DataSource
			//        .CastTo<IEntityCollection<IPublicationViewModel>>()
			//        .BulkLoad( sourceEntity.Publications, si =>
			//        {
			//            var vm = this.entityViewModelFactory.Create<IPublicationViewModel>();

			//            vm.ParentViewModel = this;
			//            vm.Parent = this.SourceEntity;

			//            vm.Initialize( si, registerAsTransient );

			//            return vm;
			//        } );
			//}






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

		//public IEntityView<AddressViewModel> Addresses
		//{
		//    get;
		//    private set;
		//}
	}
}
