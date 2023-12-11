#nullable disable
using Natural.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace NatDMS.Models
{
    public class SaveExecutiveViewModel
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "Last Name must be between 6 and 256 characters.")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "Last Name must be between 6 and 256 characters.")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address. The email domain must be @gmail.com.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Mobile Number format. Use 10 digits.")]
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
