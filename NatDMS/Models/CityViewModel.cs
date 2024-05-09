using Natural.Core.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace NatDMS.Models
{
    public partial class CityViewModel
    {
      
        public string Id { get; set; }
        public string CityName { get; set; }
        public string StateId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public List<StateModel> StateList { get; set; }



    }
}
