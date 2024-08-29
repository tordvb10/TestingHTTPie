using Microsoft.AspNetCore.Mvc;

using TestingHTTPie.Models.Base;
namespace TestingHTTPie.Models
{
    public class Hobby : CommonProperties
    {
        public string Activity { get; set; }

        public ICollection<HobbyPerson> HobbyPersons { get; set; }

    }
}