using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.Models
{
    public class DistributorLoginSample
    {
        
        public string Distributor { get; set; }
        //public string? Retailor { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-1);

        public DateTime EndDate { get; set; } = DateTime.Now;
        private string _retailor = "";

        public string Retailor
        {
            get { return _retailor; }
            set { _retailor = value ?? ""; }
        }

    }
}
