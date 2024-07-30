using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace Natural.Core.Models
{
    public class DistributorSalesReport
    {
        public string Id { get; set; }
        public string Executive { get; set; }
        public List<DsrExecutiveDrop> Executivelist { get; set; }
        public string Distributor { get; set; }
        public List<DsrDistributorDrop> Distributorlist { get; set; }
        public string Retailor { get; set; }
        public string RetailorName { get; set; }

        public List<DsrRetailorDrop> Retailorlist { get; set; }
        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }= DateTime.Now;

        [Required(ErrorMessage = "End Date is required.")]
        public DateTime EndDate { get; set; } =DateTime.Now;
        //public string StartDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        //public string EndDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public List<DistributorSalesReportInput> report { get; set; }
        public List<CityModel> CityList { get; set; }
        public List<StateModel> StateList { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }



    }
}
