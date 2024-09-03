using TestingHTTPie.Models.Base;

namespace TestingHTTPie.Models
{
    public class HobbyPerson : CommonProperties
    {
        public Guid HobbyId { get; set; }
        public Hobby Hobby { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public HobbyPerson() { }
    }
}