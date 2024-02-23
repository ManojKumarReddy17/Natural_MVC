#nullable disable
using Natural.Core.Models;

namespace NatDMS.Models
{
    public class DSRViewModel
    {
       
        public List<DsrExecutiveResourse> ExecutiveList { get; set; }

        public string Executive { get; set; }

        public string Distributor { get; set; }

        public string Retailor { get; set; }

        public List<DsrResourse> dsr { get; set; }

    }
}
