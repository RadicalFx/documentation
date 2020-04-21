namespace AutocompleteSample.BindingObjects
{
    public class Person
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }

        public string FullName { get { return FirstName + " " + LastName; } }

    }
}
