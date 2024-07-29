namespace TestingHTTPie.Models
{
    public class HobbyPerson
    {
        public Guid HobbyId { get; set; }
        public Hobby Hobby { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public HobbyPerson() { }
    }
}