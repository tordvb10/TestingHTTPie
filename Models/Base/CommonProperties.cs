using System;
namespace TestingHTTPie.Models.Base
{
    public class CommonProperties : ICommonProperties
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public byte[] RowVersion { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}