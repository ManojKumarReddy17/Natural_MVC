using System;
#nullable disable

namespace Natural.Core.Models
{
	public class DsrProduct
	{
        public string Id { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal? Total { get; set; }
        public int? Quantity { get; set; }
        public decimal? Weight { get; set; }
    }
}

