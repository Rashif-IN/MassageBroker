
using MediatR;
using notification_handler.Model;

namespace notification_handler.UseCase.Notification.Command.Delete
{
    public class Command : RequestData<notif_model>, IRequest<Dto>
    {
        public int Id { get; set; }
        public Command(int id)
        {
            Id = id;
        }
    }
}
