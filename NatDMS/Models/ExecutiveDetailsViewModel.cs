#nullable disable
using Natural.Core.Models;

namespace NatDMS.Models
{
    public class ExecutiveDetailsViewModel
    {
        public ED_CreateModel ExecutiveDetails { get; set; }
        public List<DistributorModel> AssignedDistributors { get; set; }

    }
}
