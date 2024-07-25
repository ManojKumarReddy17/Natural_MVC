using Natural.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace NatDMS.Models
{
    public class DistributorLoginViewModel
    {
        //public string Id { get; set; }
        //public string Distributor { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Retailor { get; set; }
        //  public List<DsrRetailorDrop> Retailorlist { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        // public List<DistributorSalesReportResult> report { get; set; }
       //  public string Area { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
       // public List<DsrRetailorDrop> Items { get; set; }
    }
}
