namespace TestingHTTPie.Models
{
    public class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public byte[] RowVersion { get; set; } = new byte[] { 0 }; // Initialize RowVersion property
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<HobbyPerson> HobbyPersons { get; set; }


    }
}