using System;
using static Natural.Core.Models.NotificationDistributor;

namespace Natural.Core.Models
{
	public class Notification
	{
            public string Id { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }

            public List<NotificationDistributor> distributorlist { get; set; }

        
    }
}

