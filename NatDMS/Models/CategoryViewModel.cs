
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace NatDMS.Models
{
    public class CategoryViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        public string CategoryName { get; set; }

    }
}
