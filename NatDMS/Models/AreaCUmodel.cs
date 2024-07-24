using Natural.Core.Models;

namespace NatDMS.Models
{
    public class AreaCUmodel
    {
         public string Id { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public List<AreaModel> AreaList { get; set; }
        public string AreaName { get; set; }    
        public string CityId { get; set; }
        public string State { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }


        public List<CityModel> CityList { get; set; }
        public List<StateModel> StateList { get; set; }
       
        
    }
}
