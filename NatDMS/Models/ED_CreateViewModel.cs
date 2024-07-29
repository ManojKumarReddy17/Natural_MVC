
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
#nullable disable

namespace NatDMS.Models
{

    public class ED_CreateViewModel
    {
        public List<CityModel> CityList { get; set; }
        public string Id { get; set; }

        [Required(ErrorMessage = "FirstName is Required")]
        //  [RegularExpression("[a-zA-Z]{1,40}", ErrorMessage = "must contain upto 40 alphabets only")]
        [StringLength(40, ErrorMessage = "First name cannot exceed 40 characters.")]
        public string FirstName { get; set; }

        public string CompanyName { get; set; }

       // [Required(ErrorMessage = "LastName is Required")]
        //[RegularExpression("[a-zA-Z]{1,40}", ErrorMessage = "must contain upto 40 alphabets only")]
        [StringLength(40, ErrorMessage = "LastName cannot exceed 40 characters.")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage ="Invalid email Address.The email domain must be @gmail.com")]
        [StringLength(30, ErrorMessage = "Email cannot exceed 30 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9@._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^[1-9]\d{9}$", ErrorMessage = "Invalid Mobile Number format. Use 10 digits and do not start with zero.")]
        public string MobileNumber { get; set; }

       
        [Required(ErrorMessage = "Address cannot be more than 100 characters.")]

        public string Address { get; set; }
        [Required(ErrorMessage ="City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage ="State is required.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression(@"^[a-zA-Z_]+$", ErrorMessage = "Username can only contain letters and underscores, no other special characters are allowed.")]
        [StringLength(20, ErrorMessage = " Username cannot exceed 20 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%^&*])[A-Za-z0-9!@#$%^&*]{6,15}$",
        ErrorMessage = "Passwords must be at least 6 characters and include an uppercase letter" +
        "(A-Z),alowercase letter (a-z), a digit (0-9), and a special character (!@#$%^&*)")]
        public string Password { get; set; }
        public string Image { get; set; }
        public IFormFile ProfileImage { get; set; }
        public List<StateModel> States { get; set;} 
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public List<string> Area { get; set; }

        public string PresignedUrl { get; set; }

    }

}

