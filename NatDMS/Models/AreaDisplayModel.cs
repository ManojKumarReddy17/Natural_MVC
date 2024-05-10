using NatDMS.Models;
using Natural.Core.Models;

namespace NatDMS.Models
{
    public class AreaDisplayModel
    {
        public string Id { get; set; }
        public string AreaName { get; set; }


        public string City { get; set; }
        public string CityId { get; set; }
        public string StateId { get; set; }
       
        public List<CityModel> CityList { get; set; }
        public List<StateModel> StateList { get; set; }
       

       




    }
}

