using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace Natural.Core.Models
{
    //[Serializable]
    public class AreaModel
    {
        public string Id { get; set; }
       
        public string AreaName { get; set; }
        public string CityId { get; set; }
        public string StateId { get; set; }
         public string City { get; set; }
        public string State { get; set; }
        
        public List<CityModel> CityList { get; set; }
        public List<StateModel> StateList{ get; set; }
      
        

    }
}
