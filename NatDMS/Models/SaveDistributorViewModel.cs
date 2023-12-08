using System;
using System.Collections.Generic;

#nullable disable

namespace NatDMS.Models
{
    public partial class SaveDistributorViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
      
        public string AreaId { get; set; }

        public string Area { get; set; }

        public string City { get; set; }
        public string CityId { get; set; }

        public string State { get; set; }

        public string StateId { get; set; }

            
        
       
    }
}
