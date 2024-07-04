
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace NatDMS.Models
{
    public class CategoryViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "The Categoery name  is required.")]
        [StringLength(50, ErrorMessage = " No special characters will allowed .")]
        [RegularExpression("[a-zA-Z]{1,40}", ErrorMessage = "must contain upto 40 alphabets only")]
        public string CategoryName { get; set; }


    }
}
