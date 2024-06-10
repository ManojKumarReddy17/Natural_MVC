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
       public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email Address.The email domain must be @gmail.com")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Mobile Number is required.")]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Mobile Number format. Use 10 digits.")]
        //public string MobileNumber { get; set; }
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^[1-9]\d{9}$", ErrorMessage = "Invalid Mobile Number format. Use 10 digits and do not start with zero.")]
        public string MobileNumber { get; set; }


        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(50, ErrorMessage = "Address cannot be more than 50 characters.")]

        //[StringLength(50, ErrorMessage = "The address cannot be longer than 50 characters.")]
        public string Address { get; set; }
       

        
        public string City { get; set; }

        public string CityId { get; set; }

        
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
        public string Image { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<string> Area { get; set; }
        public List<string> AreaId { get; set; }

        public string Id { get; set; }

    }
}
