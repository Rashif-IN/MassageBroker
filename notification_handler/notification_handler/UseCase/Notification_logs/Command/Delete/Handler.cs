
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace notification_handler.UseCase.Notification_logs.Command.Delete
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
            var userdata = await konteks.notiflog.FindAsync(request.Id);
            if (userdata == null)
            { return null; }
            else
            {
                konteks.notiflog.Remove(userdata);
                await konteks.SaveChangesAsync(cancellationToken);
                return new Dto
                {
                    message = "notification log removed",
                    success = true
                };
            }

        }
    }
}
