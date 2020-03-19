
using System;
using System.Collections.Generic;
using MediatR;
using notification_handler.Model;

namespace notification_handler.UseCase.Notification.Command.Put
{
    public class Command : RequestData<PutCommand>, IRequest<Dto>
    {
        
    }
    public class PutCommand
    {
        public int Notification_id { get; set; }
        public double Read_at { get; set; } = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
        public List<Target> Target { get; set; }
    }

    public class Target
    {
        public int Id { get; set; }
    }
}
