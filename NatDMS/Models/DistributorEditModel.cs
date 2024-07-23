using Natural.Core.Models;
using System.ComponentModel.DataAnnotations;
#nullable disable
namespace NatDMS.Models
{
    public class DistributorEditModel
    {

        [Required(ErrorMessage = "Company Name is required.")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Company Name must be between 3 and 40 characters.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "CompanyName must contain only Alphabets")]
        public string FirstName { get; set; }
        public string CompanyName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^[1-9]\d{9}$", ErrorMessage = "Invalid Mobile Number format. Use 10 digits and do not start with zero.")]
        public string MobileNumber { get; set; }
        [MaxLength(50, ErrorMessage = "Address cannot be more than 50 characters.")]
        public string Address { get; set; }

        public string City { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string CityId { get; set; }
        public string State { get; set; }
        [Required(ErrorMessage = "State is required")]

        public string StateId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public List<StateModel> StateList { get; set; }

        public List<CityModel> CityList { get; set; }

        public IFormFile ProfileImage { get; set; }


        //public List<AreaModel> AreaList { get; set; }
        public string PresignedUrl { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }


        //public string Area { get; set; }

        [Required(ErrorMessage = "Please select an area.")]

        //public string AreaId { get; set; }

        public string Id { get; set; }
        public string Image { get; set; }
    }
}
