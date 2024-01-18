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
              [Required(ErrorMessage = "FirstName is Required")]
              [RegularExpression("[a-zA-Z]{1,40}", ErrorMessage = "must contain upto 40 alphabets only")]
              public string FirstName { get; set; }

              [Required(ErrorMessage = "Last Name is required.")]
              [RegularExpression("[a-zA-Z]{1,40}", ErrorMessage = "must contain upto 40 alphabets only")]
              public string LastName { get; set; }

              [EmailAddress(ErrorMessage = "Invalid email address. The email domain must be @gmail.com")]
              public string Email { get; set; }

              [Required(ErrorMessage = "Mobile Number is required.")]
              [RegularExpression(@"\d{10}", ErrorMessage = "Invalid Mobile Number format. Use 10 digits.")]
              public string MobileNumber { get; set; }
              public string Address { get; set; }

              [Required(ErrorMessage = "Area is required.")]
              public string Area { get; set; }

              [Required(ErrorMessage = "City is required.")]
              public string City { get; set; }

              [Required(ErrorMessage = "State is required.")]
              public string State { get; set; }

              [Required(ErrorMessage = "Username is Required")]
              [RegularExpression("[a-zA-Z]{1,20}", ErrorMessage = "must contain upto 20 alphabets only")]
              public string UserName { get; set; }

              [Required(ErrorMessage = "Password is required")]
              [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%^&*])[A-Za-z0-9!@#$%^&*]{6,15}$",
              ErrorMessage = "Passwords must be at least 6 characters and include an uppercase letter" +
              "(A-Z),alowercase letter (a-z), a digit (0-9), and a special character (!@#$%^&*)")]
              public string Password { get; set; }
              public List<StateModel> States { get; set; }
        }
    }
