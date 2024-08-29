namespace TestingHTTPie.Dto.Base
{
    public interface ICommonPropertiesDto
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}