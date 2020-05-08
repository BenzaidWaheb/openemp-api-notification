using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenempApiNotifications.Models.RequestDTO
{
    public class UpdateRequestNotificationDTO
    {
        public Guid NotificationId { get; set; }
        public string NotificationMessage { get; set; }
        public bool IsRead { get; set; }
    }
}
