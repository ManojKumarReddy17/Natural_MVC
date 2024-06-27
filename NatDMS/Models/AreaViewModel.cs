using Natural.Core.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace NatDMS.Models
{
    public partial class AreaViewModel
    {


        public String Id { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int TotelItems { get; set; }
        public int TotalPageCount { get; set; }

        public string CityId { get; set; }
        public string StateId { get; set; }
        public List<CityModel> CityList { get; set; }
        public List<AreaModel> AreaList { get; set; }
        public List<StateModel> StateList { get; set;}
        //public List<AreaCUmodel> AreaList { get; set; }



    }
}
