
#nullable disable
using System.ComponentModel.DataAnnotations;

namespace NatDMS.Models
{
    public class DistributorViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Area { get; set; }

    }
}

