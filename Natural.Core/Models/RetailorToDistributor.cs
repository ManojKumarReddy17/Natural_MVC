using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace Natural.Core.Models
{
    public class RetailorToDistributor
    {
        

        public string Id { get; set; }
        public string DistributorId { get; set; }
        public List<string> RetailorIds { get; set; }
    }

}
