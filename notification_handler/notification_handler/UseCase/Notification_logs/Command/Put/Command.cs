
using MediatR;
using notification_handler.Model;

namespace notification_handler.UseCase.Notification_logs.Command.Put
{
    public class Command : RequestData<notif_logs_model>, IRequest<Dto>
    {
        public int Id { get; set; }
        public Command(int id)
        {
            Id = id;
        }
    }
}
