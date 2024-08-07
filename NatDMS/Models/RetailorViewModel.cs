
using System.ComponentModel.DataAnnotations;
#nullable disable
namespace NatDMS.Models
{
    public class RetailorViewModel
    {
        public string Id { get;set; }
        public string FirstName { get; set; }
        public string RetailerName { get; set; }
        
        public string ShopName { get; set; }    

        public string LastName { get; set; }

        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public int TotalItems { get; set; }
        public int TotalPageCount { get; set; }

        public string State { get; set; }

        public string City { get; set; }


        public string Area { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Image { get; set; }
       

    }
}
