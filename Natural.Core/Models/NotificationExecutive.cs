using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.Models
{
    public class NotificationExecutive
    {
        public string Executive { get; set; }
        public string Notification { get; set; }
        public static implicit operator List<object>(NotificationExecutive v)
        {
            throw new NotImplementedException();
        }
    }
}
