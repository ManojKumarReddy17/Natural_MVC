using System;
using Natural.Core.Models;
#nullable disable

namespace NatDMS.Models
{
	public class NotificationViewmodel
	{

        public List<DsrExecutiveResourse> ExecutiveList { get; set; }

        public string Executive { get; set; }

        public List<string> Distributor { get; set; }

        public string Body { get; set; }

       

        public string Subject { get; set; }

        public string Id { get; set; }

        public List<DsrDistributor> distributorlist { get; set; }



        //added while  addeing search 

        //public DateTime StartDate { get; set; }

        //public DateTime EndDate { get; set; }

        //public List<DsrResourse> dsr { get; set; }
    }
}

