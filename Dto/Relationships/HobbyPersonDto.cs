
using TestingHTTPie.Dto.Base;

namespace TestingHTTPie.Dto
{
    public class HobbyPersonDto : CommonPropertiesDto
    {
        public HobbyDtoBase Hobby { get; set; }
        public PersonDtoBase Person { get; set; }
        public HobbyPersonDto() { }
    }
}