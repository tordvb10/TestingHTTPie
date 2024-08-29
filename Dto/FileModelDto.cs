using TestingHTTPie.Dto.Base;
namespace TestingHTTPie.Dto
{
    public class FileModelDto : FileModelDtoBase
    {

    }

    public class FileModelDtoBase : CommonPropertiesDto
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}