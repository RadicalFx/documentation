using Radical.Model;

namespace MementoSimpleGraphSample.Presentation
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
	}
}
