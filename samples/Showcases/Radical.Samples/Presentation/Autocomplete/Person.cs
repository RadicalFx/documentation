namespace Radical.Samples.Presentation.Autocomplete
{
	public class Person 
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public string FullName { get { return FirstName + " " + LastName; } }

		public override string ToString()
		{
			return FirstName;
		}
	}
}
