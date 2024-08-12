using System;
using System.ComponentModel.DataAnnotations;
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
        [Range(0, 999, ErrorMessage = "Quantity must be between 0 and 999.")]
        public int? Quantity { get; set; }
        public decimal? Weight { get; set; }
    }
   

}

