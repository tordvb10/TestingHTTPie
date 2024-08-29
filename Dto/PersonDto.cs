using TestingHTTPie.Dto.Base;
namespace TestingHTTPie.Dto
{
    public class PersonDto : PersonDtoBase
    {
        public ICollection<HobbyDtoBase> Hobbies { get; set; }
    }

    public class PersonDtoBase : CommonPropertiesDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}