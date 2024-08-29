namespace TestingHTTPie.Dto
{
    public class FileModelDto : FileModelDtoBase
    {

    }

    public class FileModelDtoBase
    {
        public Guid Id { get; set; }
        public byte[] RowVersion { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    
    }
}