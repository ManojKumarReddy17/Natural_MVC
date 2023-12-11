using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace NatDMS.Models
{
    public class EditViewModel
    {
        public string Id { get; set; }
      
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please Enter First Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Please enter a valid First Name ")]
        public string? FirstName { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please Enter Last Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Please enter a valid Last Name ")]
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please Enter Mobile number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Please enter a valid Mobile Number")]
        public string? MobileNumber { get; set; }

        [StringLength(256, MinimumLength = 10, ErrorMessage = "Please enter a valid Address ")]
        public string? Address { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Selecting Area is required.")]
        public string? Area { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Selecting City  is required.")]
        public string? City { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Selecting state  is required.")]
        public string? State { get; set; }
        


        [Required(ErrorMessage = "Selecting Area is required.")]
        public string? Area { get; set; }

        [Required(ErrorMessage = "Selecting City  is required.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Selecting state  is required.")]
        public string? State { get; set; }
        

        public IEnumerable<SelectListItem>? StateList { get; set; }

        public IEnumerable<SelectListItem>? CityList { get; set; }

        public IEnumerable<SelectListItem>? AreaList { get; set; }

    }
}
