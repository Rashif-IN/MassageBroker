
using MediatR;
using notification_handler.Model;

namespace notification_handler.UseCase.Notification_logs.Command.Post
{
    public class Command : RequestData<notif_logs_model>, IRequest<Dto>
    {

    }
}
