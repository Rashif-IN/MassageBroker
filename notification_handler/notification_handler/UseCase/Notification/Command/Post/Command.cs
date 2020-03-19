
using System.Collections.Generic;
using MediatR;
using notification_handler.Model;

namespace notification_handler.UseCase.Notification.Command.Post
{
    public class Command : RequestData<PostCommand> ,IRequest<Dto>
    {

    }

    public class PostCommand
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public int From { get; set; }
        public List<TargetCommand> Target { get; set; }
    }

    public class TargetCommand
    {
        public int Id { get; set; }
        public string Email_destination { get; set; }
    }
}
