#nullable disable
using Natural.Core.Models;

namespace NatDMS.Models
{
    public class ExecutiveDetailsViewModel
    {
        public ExecutiveViewModel ExecutiveDetails { get; set; }
        public List<DistributorModel> AssignedDistributors { get; set; }

    }
}
