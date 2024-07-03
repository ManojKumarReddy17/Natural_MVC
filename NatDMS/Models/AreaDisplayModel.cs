using NatDMS.Models;
using Natural.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace NatDMS.Models
{
    public class AreaDisplayModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "FirstName is Required")]
        [RegularExpression("[a-zA-Z]{1,40}", ErrorMessage = "must contain up to 40 alphabets only")]
        public string AreaName { get; set; }


        public string City { get; set; }
        public string CityId { get; set; }
        public string StateId { get; set; }
       
        public List<CityModel> CityList { get; set; }
        public List<StateModel> StateList { get; set; }
       public List<AreaModel> AreaList { get; set; }

       




    }
}

