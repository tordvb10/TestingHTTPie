namespace TestingHTTPie.Models
{
    public class Hobby
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public byte[] RowVersion { get; set; } = new byte[] { 0 }; // Initialize RowVersion property
        public string Activity { get; set; }

        public ICollection<HobbyPerson> HobbyPersons { get; set; }


    }
}