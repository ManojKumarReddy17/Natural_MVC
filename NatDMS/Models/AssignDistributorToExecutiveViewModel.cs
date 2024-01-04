#nullable disable

namespace NatDMS.Models
{
    public class AssignDistributorToExecutiveViewModel
    {
        public string ExecutiveId { get; set; }

        //  public string DistributorId { get; set; }
        public List<string> DistributorIds { get; set; }
    }
}
