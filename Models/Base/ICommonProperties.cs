namespace TestingHTTPie.Models.Base
{
    public interface ICommonProperties
    {
        public Guid Id { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}