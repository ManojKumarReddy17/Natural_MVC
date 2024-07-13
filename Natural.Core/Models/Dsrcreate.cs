using System;
#nullable disable
namespace Natural.Core.Models
{
	public class Dsrcreate
	{
        public List<DsrExecutiveDrop> ExecutiveList { get; set; }

        public string Executive { get; set; }

        public List<DsrDistributorDrop> DistributorList { get; set; }

        public string Distributor { get; set; }

        public List<DsrRetailorDrop> RetailorList { get; set; }

        public string Retailor { get; set; }
        public string Retailorid { get; set; }

        public List<CategoryModel> CategoryList { get; set; }

        public string Category { get; set; }

        public List<DsrProduct> ProductList { get; set; }

        public string Product { get; set; }

        public string dsrid { get; set; } // added for edit

        public DateTime CreatedDate { get; set; }
    }
}

