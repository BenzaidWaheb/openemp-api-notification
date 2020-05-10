using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenempApiNotifications.Models.RequestDTO
{
    public class UpdateRequestNotificationDTO
    {
        public bool IsRead { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
