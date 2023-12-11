
using System.ComponentModel.DataAnnotations;
#nullable disable
namespace NatDMS.Models
{
    public class RetailorViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobilenumber is required")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public string StateId { get; set; }
        [Required(ErrorMessage = "State is required")]

        public string State { get; set; }
        [Required]
        public string CityId { get; set; }
        [Required(ErrorMessage = "City is required")]

        public string City { get; set; }
        [Required]
        public string AreaId { get; set; }

        [Required(ErrorMessage = "Area is required")]

        public string Area { get; set; }
        
    }
}
