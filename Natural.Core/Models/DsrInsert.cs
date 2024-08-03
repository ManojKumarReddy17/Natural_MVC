using System;
#nullable disable
namespace Natural.Core.Models
{
	public class DsrInsert
	{
        public string Area { get; set; }
        public string Executive { get; set; }
        public string Distributor { get; set; }
        public string Retailor { get; set; }
        public string OrderBy { get; set; }
        public decimal? TotalAmount { get; set; }
        public string dsrid { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<DsrInsertProduct> product { get; set; }
    }
}

