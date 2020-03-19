
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using notification_handler.Model;

namespace notification_handler.UseCase.Notification.Query.Get
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
            var dataNotif = await konteks.notif.FirstOrDefaultAsync(X => X.id == request.Id);
            var dataNotifLog = await konteks.notiflog.Where(X => X.notification_id == request.Id).ToListAsync();

            var listLog = new List<NotificationLogData>();

            foreach(var X in dataNotifLog)
            {
                listLog.Add(new NotificationLogData()
                {
                    Notification_id = X.notification_id,
                    From = X.from,
                    Read_at = X.read_at,
                    Target = X.target
                });
            }

            if (dataNotif != null)
            {
                return new Dto()
                {
                    message = "notification retrieved",
                    success = true,
                    Data = new NotificationDTO()
                    {
                        Notifications = new NotificationData()
                        {
                            Id = dataNotif.id,
                            Title = dataNotif.title,
                            Message = dataNotif.message
                        },
                        Notification_logs = listLog
                    }
                };
            }
            else { return null; }
        }
    }
}
