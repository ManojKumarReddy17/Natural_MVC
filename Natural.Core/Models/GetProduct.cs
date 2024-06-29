using System;
#nullable disable
namespace Natural.Core.Models
{
    public class GetProduct
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public string Image { get; set; }
        public string ProductType { get; set; }

        // public string PresignedUrl { get; set; }
    }
}