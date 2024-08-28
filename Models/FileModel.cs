using Microsoft.AspNetCore.Mvc;

namespace TestingHTTPie.Models
{ 
    public class FileModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
