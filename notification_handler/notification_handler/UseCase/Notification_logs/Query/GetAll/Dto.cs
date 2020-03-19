
using System.Collections.Generic;
using notification_handler.Model;

namespace notification_handler.UseCase.Notification_logs.Query.GetAll
{
    public class Dto : dto_model
    {
        public List <NotificationDTO> Data { get; set; }
    }
}
