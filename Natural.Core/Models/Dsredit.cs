using System;
namespace Natural.Core.Models
{
    public class Dsredit
    {
        public string Executive { get; set; }
        public string Distributor { get; set; }
        public string Retailor { get; set; }
        public string? OrderBy { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<DsrProduct> dsrdetail {get; set;}
    }
}

