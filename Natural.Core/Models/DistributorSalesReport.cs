using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace Natural.Core.Models
{
    public class DistributorSalesReport
    {
        public string Executive { get; set; }
        public List<DsrExecutiveDrop> Executivelist { get; set; }
        public string Distributor { get; set; }
        public List<DsrDistributorDrop> Distributorlist { get; set; }

        public string Retailor { get; set; }
        public List<DsrRetailorDrop> Retailorlist { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public List<DistributorSalesReportInput> report { get; set; }
    }
}
