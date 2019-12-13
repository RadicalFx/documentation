namespace Radical.Samples.Presentation.Autocomplete
{
	public class Person //: 
		//AutoComplete.ICanRepresentMyself,
		//AutoComplete.IHaveAnOpinionOnFilter
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public string FullName { get { return FirstName + " " + LastName; } }

		public override string ToString()
		{
			return FirstName;
		}

		//string AutoComplete.ICanRepresentMyself.AsString()
		//{
		//	return this.FullName;
		//}

		//bool AutoComplete.IHaveAnOpinionOnFilter.Match( string userText )
		//{
		//	return this.FullName.StartsWith( userText, StringComparison.OrdinalIgnoreCase );
		//}
	}
}
