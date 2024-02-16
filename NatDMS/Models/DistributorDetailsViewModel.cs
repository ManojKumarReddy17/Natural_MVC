using Natural.Core.Models;


#nullable disable
namespace NatDMS.Models
{
    public class DistributorDetailsViewModel
    {

        public DistributorViewModel DistributorDetails { get; set; }

        public List<RetailorModel> AssignedRetailors { get; set; }


    }
}
