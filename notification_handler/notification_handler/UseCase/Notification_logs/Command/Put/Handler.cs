using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace notification_handler.UseCase.Notification_logs.Command.Put
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
            var notifdata = konteks.notiflog.Find(request.data.Attributes.id);
            notifdata.type = request.data.Attributes.type;
            notifdata.email_destination = request.data.Attributes.email_destination;
            notifdata.updated_at = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;

            await konteks.SaveChangesAsync(cancellationToken);
            return new Dto
            {
                message = "notification log updated",
                success = true
            };


        }
    }
}
