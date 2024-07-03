using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace NatDMS.Models
{
    public partial class CityViewModel
    {
      
        public string Id { get; set; }

        [Required(ErrorMessage = "CityName is Required")]
        [RegularExpression("[a-zA-Z]{1,40}", ErrorMessage = "must contain up to 40 alphabets only")]
        public string CityName { get; set; }
        public string StateId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public List<StateModel> StateList { get; set; }



    }
}
