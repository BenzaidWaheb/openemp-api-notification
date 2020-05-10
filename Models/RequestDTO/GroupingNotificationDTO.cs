
using System;

namespace OpenempApiNotification.Models.RequestDTO
{
    public class GroupingNotificationDTO
    {
        private int rowCounts = 8;

        public int RowCounts { get => rowCounts; set => rowCounts = Math.Min(10, value); }
        public int GroupNumber { get; set; }
    }
}