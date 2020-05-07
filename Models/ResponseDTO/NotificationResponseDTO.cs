using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenempApiNotifications.Models.ResponseDTO
{
    public class NotificationResponseDTO
    {
        public string NotificationMessage { get; set; }
        public string Reciever { get; set; }
        public string Sender { get; set; }
        public bool IsRead { get; set; }
        public string DeletedBy { get; set; }
        public int NotificationCount { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
