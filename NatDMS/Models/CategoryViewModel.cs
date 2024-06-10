
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace NatDMS.Models
{
    public class CategoryViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "The Cateogery name  is required.")]
        [StringLength(50, ErrorMessage = " No special characters will allowed .")]
        public string CategoryName { get; set; }


    }
}
