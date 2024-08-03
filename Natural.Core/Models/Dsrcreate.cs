using System;
using System.ComponentModel.DataAnnotations;
#nullable disable
namespace Natural.Core.Models
{
	public class Dsrcreate
	{
        public List<DsrExecutiveDrop> ExecutiveList { get; set; }
        public string Area { get; set; }
        public List<AreaModel> AreaList { get; set; }
        [Required(ErrorMessage = "Executive is required.")]
        public string Executive { get; set; }
       
        public List<DsrDistributorDrop> DistributorList { get; set; }
        [Required(ErrorMessage = "Distributor is required.")]
        public string Distributor { get; set; }

        public List<DsrRetailorDrop> RetailorList { get; set; }
        [Required(ErrorMessage = "Retailor is required.")]
        public string Retailor { get; set; }
        public string Retailorid { get; set; }
        public string RetailorName { get; set; }

        public List<CategoryModel> CategoryList { get; set; }

        public string Category { get; set; }

        public List<DsrProduct> ProductList { get; set; }

        public string Product { get; set; }

        public string dsrid { get; set; } // added for edit
        [Required(ErrorMessage = "CreatedDate is required.")]
        public DateTime CreatedDate { get; set; }

        public List<CityModel> CityList { get; set; }
        public List<StateModel> StateList { get; set; }
    
        public string City { get; set; }
        public string State { get; set; }
    }
}

