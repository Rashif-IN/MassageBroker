using System;
namespace notification_handler.Model
{
    public class notif_model
    {
        public int id { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public double created_at { get; set; } = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
        public double updated_at { get; set; } = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
    }
}
