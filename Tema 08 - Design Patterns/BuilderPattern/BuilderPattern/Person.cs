namespace BuilderPattern
{
    public class Person
    {
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; }
        public string IdNumber { get; }

        public Person(string idNumber, string name, DateTime? dateOfBirth = null)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            IdNumber = idNumber;
        }
    }
}
