namespace TestingHTTPie.Models
{
    public class Hobby
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public byte[] RowVersion { get; set; } = new byte[] { 0 }; // Initialize RowVersion property
        public string hobby { get; set; }

    }
}