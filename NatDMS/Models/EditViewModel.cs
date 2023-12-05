using Microsoft.AspNetCore.Mvc.Rendering;

namespace NatDMS.Models
{
    public class EditViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public string SelectedState { get; set; }
        public string SelectedCity { get; set; }

        public string SelectedArea { get; set; }

        public IEnumerable<SelectListItem> StateList { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }

        public IEnumerable<SelectListItem> AreaList { get; set; }

    }
}
