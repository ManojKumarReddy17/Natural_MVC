using Microsoft.AspNetCore.Mvc.Rendering;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace NatDMS.Models
{
    public class ED_EditViewModel
    {
        [Required(ErrorMessage = "Company Name is required.")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Company Name must be between 3 and 40 characters.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "CompanyName must contain only Alphabets")]
        public string FirstName { get; set; }
        public string CompanyName { get; set; }


        //[Required(ErrorMessage = "Last Name is required.")]
        //[StringLength(40, MinimumLength = 3, ErrorMessage = "Last Name must be between 3 and 40 characters.")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address. The email domain must be @gmail.com.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Mobile Number format. Use 10 digits.")]
        public string MobileNumber { get; set; }

        public string Address { get; set; }

        //[Required(ErrorMessage = "Area is required.")]
        //public string Area { get; set; }

        //[Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        public string CityId { get; set; }

        //[Required(ErrorMessage = "State is required.")]
        public string State { get; set; }
        public string StateId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } 
        
        public List<StateModel> StateList { get; set; }

        public List<CityModel> CityList { get; set; }

        public IFormFile ProfileImage { get; set; }


        public List<AreaModel> AreaList { get; set; }
       public string PresignedUrl { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        //[Required(ErrorMessage = "Area is required.")]
        public List<string> Area { get; set; }
        public List<string> AreaId { get; set; }

        public string Id { get; set; }

    }
}
