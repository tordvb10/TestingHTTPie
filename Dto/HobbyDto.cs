using TestingHTTPie.Dto.Base;
namespace TestingHTTPie.Dto
{
    public class HobbyDto : HobbyDtoBase
    {
        public HobbyDto() { }
        public ICollection<PersonDtoBase> Persons { get; set; }
    }

    public class HobbyDtoBase : CommonPropertiesDto 
    {
        public string Activity { get; set; }
    }
}
