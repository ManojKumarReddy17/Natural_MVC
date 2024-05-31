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

        public string Retailor { get; set; }
        public List<RetailorByArea> Retailorlist { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public List<StateModel> StateList { get; set; }

        public List<DistributorSalesReportInput> report { get; set; }
    }
}
