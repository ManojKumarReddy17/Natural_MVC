using NatDMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.Models
{
    public class DistributorLoginModel
    {
        public string Id { get; set; }
        public string Distributor { get; set; }

        public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string FullName
        //{
        //    get { return $"{FirstName} {LastName}"; }
        //}
        public string exeId { get; set; }
        public string executives { get; set; }
        public string Retailor { get; set; }
        public string RetailorId { get; set; }
        public string RetailorName { get; set; }
        public List<DsrDistributorDrop> Distributorlist { get; set; }
        public List<DsrRetailorDrop> Retailorlist { get; set; }
        public string SelectedDistributorId { get; set; }
        public string SelectedRetailorId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now.Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;

        public List<DistributorSalesReportInput> report { get; set; }
        public string Area { get; set; }

        public string AreaName { get; set; }
        public List<AreaModel> Areas { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        public List<DsrRetailorDrop> Items { get; set; }
    }
}
