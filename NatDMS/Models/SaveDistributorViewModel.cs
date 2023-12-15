using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace NatDMS.Models
{

        public class SaveDistributorViewModel
        {
          [Required(ErrorMessage = "FirstName is Required")]
          [RegularExpression("[a-zA-Z]{1,40}", ErrorMessage = "must contain upto 40 alphabets only")]

        public string FirstName { get; set; }

            [Required(ErrorMessage = "Last Name is required.")]
            [RegularExpression("[a-zA-Z]{1,40}", ErrorMessage = "must contain upto 40 alphabets only")]
            public string LastName { get; set; }

            [EmailAddress(ErrorMessage = "Invalid email address. The email domain must be @gmail.com.")]
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

            [Required(ErrorMessage = "Username is required.")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            public string Password { get; set; }

           public List<StateModel> States { get; set; }
        }
    }
