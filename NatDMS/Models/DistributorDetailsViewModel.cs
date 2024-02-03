using Natural.Core.Models;


#nullable disable
namespace NatDMS.Models
{
    public class DistributorDetailsViewModel
    {

        public DistributorViewModel Distributors { get; set; }

        public List<AssignedRetailorToDistributorIdModel> AssignedRetailors { get; set; }


    }
}
