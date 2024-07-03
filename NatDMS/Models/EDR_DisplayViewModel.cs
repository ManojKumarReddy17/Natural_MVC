using Microsoft.AspNetCore.Mvc.Rendering;
using Natural.Core.Models;
using System.ComponentModel.DataAnnotations;


#nullable disable
namespace NatDMS.Models
{
    public class EDR_DisplayViewModel
    {

        public string Id { get; set; }

        [RegularExpression("[a-zA-Z]{1,40}", ErrorMessage = "must contain upto 40 alphabets only")]
       // public string FirstName { get; set; }

       // [RegularExpression("[a-zA-Z]{1,40}", ErrorMessage = "must contain upto 40 alphabets only")]
        //public string LastName { get; set; }

       // [RegularExpression("[a-zA-Z]{1,40}", ErrorMessage = "must contain upto 40 alphabets only")]
        public string FullName { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
       
        public List<StateModel> StateList { get; set; }
        public List<DistributorModel> DistributorList { get; set; }
        public List<RetailorModel> RetailorList { get; set; }
        public string Image { get; set; }
        public IFormFile ProfileImage { get; set; }
        public List<ED_CreateViewModel> ExecutiveList { get; set; }
        public int CurrentPage {  get; set; }
        public int TotalPageCount { get; set; }
         public int TotalPages {  get; set; }
        public int TotalItems { get; set; }

       

    }
}
