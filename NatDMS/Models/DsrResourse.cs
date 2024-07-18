using System;
#nullable disable
namespace NatDMS.Models
{
	public class DsrResourse
	{
        public string Id { get; set; }
        public string Executive { get; set; }
        public string Distributor { get; set; }
        public string Retailor { get; set; }
        public string OrderBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

