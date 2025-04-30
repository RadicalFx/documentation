namespace AutocompleteSample.BindingObjects
{
    public class Person
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string Superhero { get; internal set; }

        public string FullName => $"{FirstName} {LastName} : {Superhero}";
    }
}
