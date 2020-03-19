
using MediatR;

namespace notification_handler.UseCase.Notification.Query.Get
{
    public class Command : IRequest<Dto>
    {
        public int Id { get; set; }
        public Command(int id)
        {
            Id = id;
        }
    }
}
