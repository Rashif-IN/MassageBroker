using System;
using System.Collections.Generic;

namespace notification_handler.Model
{
    public class notif_logs_model
    {
        public int id { get; set; }
        public int notification_id { get; set; }
        public string type { get; set; }
        public int from { get; set; }
        public int target {get;set;}
        public string email_destination { get; set; }
        public double read_at { get; set; } = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
        public double created_at { get; set; } = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
        public double updated_at { get; set; } = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
        public notif_model notification { get; set; }
    }
   
}
