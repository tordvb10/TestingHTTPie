namespace TestingHTTPie.Dto
{
    public class HobbyDto : HobbyDtoBase
    {
        public HobbyDto() { }
        public ICollection<PersonDtoBase> Persons { get; set; }
    }

    public class HobbyDtoBase 
    {
        public Guid Id { get; set; }
        public string Activity { get; set; }
    }
}