using Microsoft.AspNetCore.Mvc;

namespace TestingHTTPie.Models
{ 
    public class FileModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public byte[] RowVersion { get; set; } = new byte[] { 0 }; // Initialize RowVersion property

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
