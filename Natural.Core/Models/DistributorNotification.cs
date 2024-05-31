using System;
#nullable disable
namespace Natural.Core.Models
{
	public class DistributorNotification
	{
        public int Id { get; set; }
        public string Distributor { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}

