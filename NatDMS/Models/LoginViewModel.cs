using System.ComponentModel.DataAnnotations;
using System.Data.Common;

#nullable disable
namespace NatDMS.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName {get; set;}
        [Required]
     public string Password { get; set; } 
    }
}
