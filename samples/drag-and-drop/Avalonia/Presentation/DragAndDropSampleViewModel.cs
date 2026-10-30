using Radical.Avalonia.Presentation;
using Radical.Avalonia.Behaviors;
using Radical.Avalonia.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DragAndDropSample.Presentation
{
	class DragAndDropSampleViewModel : AbstractViewModel
	{
		public DragAndDropSampleViewModel()
		{
			LeftPersons = new ObservableCollection<Person>() 
			{
				new Person{ FirstName= "Mauro", LastName= "Servienti" },
				new Person{ FirstName= "Giovanni", LastName= "Rossi" },
				new Person{ FirstName= "Marco", LastName= "Verdi" },
			};
			RightPersons = new ObservableCollection<Person>();

            var dd = DelegateCommand.Create()
                .OnCanExecute( state =>
                {
                    var e = ( DragOverArgs )state;

                    X = e.Position.X;
                    Y = e.Position.Y;

                    return true;
                } )
                .OnExecute( state => 
                {
                    var e = ( DropArgs )state;

                    if( e.GetData( "left/person" ) is Person p )
                    {
                        //dragging from left to right
                        LeftPersons.Remove( p );
                        RightPersons.Add( p );
                        if( LeftSelectedPerson == p )
                        {
                            LeftSelectedPerson = null;
                        }
                        RightSelectedPerson = p;
                    }
                    else if( e.GetData( "right/person" ) is Person q )
                    {
                        //dragging from right to left
                        RightPersons.Remove( q );
                        LeftPersons.Add( q );
                        if( RightSelectedPerson == q )
                        {
                            RightSelectedPerson = null;
                        }
                        LeftSelectedPerson = q;
                    }
                    else
                    {
                        //skip
                    }
                } );

            DropPerson = dd;
		}

        public ICommand DropPerson { get; private set; }

		public ObservableCollection<Person> LeftPersons { get; private set; }
		public Person LeftSelectedPerson
		{
			get { return this.GetPropertyValue( () => LeftSelectedPerson ); }
			set { this.SetPropertyValue( () => LeftSelectedPerson, value ); }
		}

		public ObservableCollection<Person> RightPersons { get; private set; }
		public Person RightSelectedPerson
		{
			get { return this.GetPropertyValue( () => RightSelectedPerson ); }
			set { this.SetPropertyValue( () => RightSelectedPerson, value ); }
		}

        public double X
        {
            get { return this.GetPropertyValue( () => X ); }
            private set { this.SetPropertyValue( () => X, value ); }
        }

        public double Y
        {
            get { return this.GetPropertyValue( () => Y ); }
            private set { this.SetPropertyValue( () => Y, value ); }
        }
	}
}
