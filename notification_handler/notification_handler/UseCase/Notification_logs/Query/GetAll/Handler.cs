
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using notification_handler.Model;

namespace notification_handler.UseCase.Notification_logs.Query.GetAll
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
            var notificationData = await konteks.notif.ToListAsync();
            var notificationLogData = await konteks.notiflog.ToListAsync();

            var notifList = new List<NotificationDTO>();

            foreach (var x in notificationData)
            {
                var logList = new List<NotificationLogData>();
                var logs = notificationLogData.Where(y => y.notification_id == x.id);
                foreach (var y in logs)
                {
                    logList.Add(new NotificationLogData
                    {
                        Notification_id = y.notification_id,
                        From = y.from,
                        Read_at = y.read_at,
                        Target = y.target
                    });
                }
                notifList.Add(new NotificationDTO()
                {
                    Notifications = new NotificationData()
                    {
                        Id = x.id,
                        Title = x.title,
                        Message = x.message
                    },
                    Notification_logs = logList
                });
            }

            return new Dto
            {
                message = "notifications retrieved",
                success = true,
                Data = notifList
            };
        }
    }
}
