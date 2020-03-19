
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace notification_handler.UseCase.Notification.Command.Delete
{
    public class Handler : IRequestHandler<Command, Dto>
    {
        private readonly Context konteks;

        public Handler(Context context)
        {
            konteks = context;
        }

        public async Task<Dto> Handle(Command request, CancellationToken cancellationToken)
        {
            var userdata = await konteks.notif.FindAsync(request.Id);
            if (userdata == null)
            { return null; }
            else
            {
                konteks.notif.Remove(userdata);
                await konteks.SaveChangesAsync(cancellationToken);
                return new Dto
                {
                    message = "notification removed",
                    success = true
                };
            }

        }
    }
}
