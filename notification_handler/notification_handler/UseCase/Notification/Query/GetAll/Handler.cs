
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using notification_handler.Model;

namespace notification_handler.UseCase.Notification.Query.GetAll
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
            var notifData = await konteks.notif.ToListAsync();
            var result = new List<NotificationData>();

            foreach(var X in notifData)
            {
                result.Add(new NotificationData
                {
                    Id = X.id,
                    Title = X.title,
                    Message = X.message
                });
            }

            return new Dto
            {
                message = "notifications retrieved",
                success = true,
                Data = result
            };
        }
    }
}
