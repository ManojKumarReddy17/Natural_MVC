using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace Natural.Core.Models
{
    public class RetailorModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
       public string LastName { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }

        public string Address { get; set; }
        public string State { get; set; }
        public string StateId { get; set; }
        public string City { get; set; }
        public string CityId { get; set; }
        public string Area { get; set; }
        public string AreaId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PresignedUrl { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string Image { get; set; }
    }
}
