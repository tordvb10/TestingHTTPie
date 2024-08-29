using Microsoft.AspNetCore.Mvc;

using TestingHTTPie.Models.Base;
namespace TestingHTTPie.Models
{ 
    public class FileModel : CommonProperties
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
