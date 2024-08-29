using Microsoft.AspNetCore.Mvc;

namespace TestingHTTPie.Models
{
	public interface ICommonProps
	{
		public Guid Id { get; set; }
		public byte[] RowVersion { get; set; }
		public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}