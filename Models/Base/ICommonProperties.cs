namespace TestingHTTPie.Models.Base
{
    public interface ICommonProperties
    {
        Guid Id { get; set; }
        byte[] RowVersion { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}