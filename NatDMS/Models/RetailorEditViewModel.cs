using Natural.Core.Models;
using System.ComponentModel.DataAnnotations;
#nullable disable
namespace NatDMS.Models
{
    public class RetailorEditViewModel
    {
        [Required(ErrorMessage = "RetailerName is required.")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Shop Name must be between 3 and 40 characters.")]
        public string ShopName { get; set; }
        public string FirstName { get; set; }
        public string RetailerName { get; set; }

       public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email Address.The email domain must be @gmail.com")]
        [StringLength(30, ErrorMessage = "Email cannot exceed 30 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Mobile Number format. Use 10 digits.")]
        public string MobileNumber { get; set; }
        [StringLength(50, ErrorMessage = "The address cannot be longer than 50 characters.")]
        public string Address { get; set; }

        //[Required(ErrorMessage = "Area is required.")]
        //public string AreaId { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string CityId { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string StateId { get; set; }

        public List<StateModel> States { get; set; }
        public List<CityModel> Cities { get; set; }
        //public List<AreaModel> Areas { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string Image { get; set; }
    }

}

