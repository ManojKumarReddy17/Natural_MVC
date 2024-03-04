using System;
#nullable disable
namespace Natural.Core.Models
{
	public class DsrInsertProduct
	{
        public string Product { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public int Id { get; set; }  //check for insert
        public string Category { get; set; }
        public string Dsr { get; set; }

    }
}

