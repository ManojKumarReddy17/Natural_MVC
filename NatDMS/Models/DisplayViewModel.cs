using Microsoft.AspNetCore.Mvc.Rendering;
using Natural.Core.Models;


#nullable disable
namespace NatDMS.Models
{
    public class DisplayViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public List<StateModel> StateList { get; set; }
        public List<ExecutiveModel> ExecutiveList { get; set; }
        public List<DistributorModel> DistributorList { get; set; }
        public List<RetailorModel> RetailorList { get; set; }
      

    }
}
