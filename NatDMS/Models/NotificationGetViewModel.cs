using System;
namespace NatDMS.Models
{
	public class NotificationGetViewModel
	{
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<NotificationViewmodel> notification { get; set; }
    }
}

