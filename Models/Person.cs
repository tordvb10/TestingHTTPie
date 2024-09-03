using TestingHTTPie.Models.Base;
namespace TestingHTTPie.Models
{
    public class Person : CommonProperties
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<HobbyPerson> HobbyPersons { get; set; } = new List<HobbyPerson>();

    }
}