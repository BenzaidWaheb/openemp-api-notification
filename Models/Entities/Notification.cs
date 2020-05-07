using System;
using System.ComponentModel.DataAnnotations;

namespace OpenempApiNotifications.Models
{
    public class Notification
    {
        public Guid NotificationId { get; set; }
        [Required]
        public string NotificationMessage { get; set; }
        [Required]
        public string Reciever { get; set; }
        [Required]
        public string Sender { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public int NotificationCount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
