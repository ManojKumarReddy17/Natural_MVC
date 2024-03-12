using System;

namespace Natural.Core.Models
{
	public class NotificationDistributor
	{
        
            public string Distributor { get; set; }
            public string Notification { get; set; }

        public static implicit operator List<object>(NotificationDistributor v)
        {
            throw new NotImplementedException();
        }
    }
}

