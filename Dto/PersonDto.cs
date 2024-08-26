namespace TestingHTTPie.Dto
{
    public class PersonDto : PersonDtoBase
    {
        public ICollection<HobbyDtoBase> Hobbies { get; set; }
    }

    public class PersonDtoBase
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}