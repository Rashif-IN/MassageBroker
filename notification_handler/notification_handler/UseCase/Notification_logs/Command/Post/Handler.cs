
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using notification_handler.Model;

namespace notification_handler.UseCase.Notification_logs.Command.Post
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
            var userdata = new notif_logs_model();
            konteks.notiflog.Add(userdata);
            await konteks.SaveChangesAsync(cancellationToken);
            return new Dto
            {
                message = "notification log posted",
                success = true
            };
        }
    }
}
