namespace TestingHTTPie.Dto
{
    public class HobbyDto
    {
        public Guid Id { get; set; }
        public string Activity { get; set; }
        public ICollection<PersonDto> Persons { get; set; }

        //public ICollection<HobbyPersonDto> HobbyPersonDtos { get; set; }


    }
}