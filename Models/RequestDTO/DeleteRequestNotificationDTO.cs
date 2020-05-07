using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenempApiNotification.Models.RequestDTO
{
    public class DeleteRequestNotificationDTO
    {
        public Guid NotificationId { get; set; }
        public string NotificationMessage { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DeletedOn { get; set; }
    }
}
