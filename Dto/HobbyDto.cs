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

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string Activity { get; set; }
    }
}