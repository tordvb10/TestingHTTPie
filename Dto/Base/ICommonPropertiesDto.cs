namespace TestingHTTPie.Dto.Base
{
    public class interface ICommonPropertiesDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}