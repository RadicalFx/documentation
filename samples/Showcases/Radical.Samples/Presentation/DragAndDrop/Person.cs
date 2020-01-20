namespace Radical.Samples.Presentation.DragAndDrop
{
	class Person
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public override string ToString()
		{
			return FirstName + " " + LastName;
		}
	}
}
