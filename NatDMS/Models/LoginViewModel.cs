using System.ComponentModel.DataAnnotations;
using System.Data.Common;

#nullable disable
namespace NatDMS.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
