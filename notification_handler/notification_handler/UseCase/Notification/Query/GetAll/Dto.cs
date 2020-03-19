
using System.Collections.Generic;
using notification_handler.Model;

namespace notification_handler.UseCase.Notification.Query.GetAll
{
    public class Dto : dto_model
    {
        public List <NotificationData> Data { get; set; }
    }
}
