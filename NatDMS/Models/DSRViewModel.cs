
using Natural.Core.Models;

#nullable disable
namespace NatDMS.Models
{
    public class DSRViewModel
    {
       
        public List<DsrExecutiveResourse> ExecutiveList { get; set; }

        public string Executive { get; set; }

        public string Distributor { get; set; }

        public string Retailor { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<DsrResourse> dsr { get; set; }

    }
}
