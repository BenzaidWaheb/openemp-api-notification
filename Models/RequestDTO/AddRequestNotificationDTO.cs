using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OpenempApiNotifications.Models.RequestDTO
{
    public class AddRequestNotificationDTO
    {
        [Required]
        public string NotificationMessage { get; set; }
        [Required]
        public string Sender { get; set; }
        [Required]
        public string Reciever { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; } //CreatedAt
    }
}
