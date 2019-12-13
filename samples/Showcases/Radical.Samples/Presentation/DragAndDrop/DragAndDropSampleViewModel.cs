using Radical.Samples.ComponentModel;
using Radical.Windows.Behaviors;
using Radical.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Radical.Samples.Presentation.DragAndDrop
{

    [Sample( Title = "Drag & Drop", Category = Categories.Behaviors )]
	class DragAndDropSampleViewModel : SampleViewModel
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

                    if( e.Data.GetDataPresent( "left/person" ) )
                    {
                        //dragging from left to right
                        var p = ( Person )e.Data.GetData( "left/person" );
                        LeftPersons.Remove( p );
                        RightPersons.Add( p );
                        if( LeftSelectedPerson == p )
                        {
                            LeftSelectedPerson = null;
                        }
                        RightSelectedPerson = p;
                    }
                    else if( e.Data.GetDataPresent( "right/person" ) )
                    {
                        //dragging from right to left 
                        var p = ( Person )e.Data.GetData( "right/person" );
                        RightPersons.Remove( p );
                        LeftPersons.Add( p );
                        if( RightSelectedPerson == p )
                        {
                            RightSelectedPerson = null;
                        }
                        LeftSelectedPerson = p;
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
