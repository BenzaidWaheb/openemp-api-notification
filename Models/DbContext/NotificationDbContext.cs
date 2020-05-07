using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenempApiNotifications.Models
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Notification> Notification { get; set; }
    }
}
