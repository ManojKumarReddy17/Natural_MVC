
using Natural.Core.Models;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace NatDMS.Models
{
    public class DSRViewModel
    {
       
        public List<DsrExecutiveResourse> ExecutiveList { get; set; }
        public string Area { get; set; }
        public List<AreaModel> AreaList { get; set; }
        public string Executive { get; set; }

        public string Distributor { get; set; }

        public string Retailor { get; set; }

        //public DateTime StartDate { get; set; }

        //public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        public DateTime EndDate { get; set; }

        public List<DsrResourse> dsr { get; set; }

        public List<CityModel> CityList { get; set; }
        public List<StateModel> StateList { get; set; }

        public string City { get; set; }
        public string State { get; set; }

    }
}
