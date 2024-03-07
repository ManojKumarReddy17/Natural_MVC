#nullable disable
namespace NatDMS.Models
{
    public class DistributorSalesReportViewModel
    {
        public string Executive { get; set; }
        public string Distributor { get; set; }
        public string Retailor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
