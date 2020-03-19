 using System;
using System.Collections.Generic;

namespace notification_handler.Model
{
    public class NotificationDTO
    {
        public NotificationData Notifications { get; set; }
        public List<NotificationLogData> Notification_logs { get; set; }
    }

    public class NotificationData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public class NotificationLogData
    {
        public int Notification_id { get; set; }
        public int From { get; set; }
        public int Target { get; set; }
        public double Read_at { get; set; } = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;

    }
}
