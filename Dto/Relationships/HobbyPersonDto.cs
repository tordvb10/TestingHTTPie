namespace TestingHTTPie.Dto
{
    public class HobbyPersonDto
    {
        public Guid HobbyId { get; set; }
        public HobbyDto Hobby { get; set; }
        public Guid PersonId { get; set; }
        public PersonDto Person { get; set; }
        public HobbyPersonDto() { }
    }
}|