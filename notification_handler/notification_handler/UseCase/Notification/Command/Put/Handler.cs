
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace notification_handler.UseCase.Notification.Command.Put
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
            var lognotif = konteks.notiflog.ToList();

            var queries = lognotif.Where(x => x.notification_id == request.data.Attributes.Notification_id);

            foreach (var x in request.data.Attributes.Target)
            {
                var data = queries.First(y => y.target == x.Id).id;
                var dataContext = await konteks.notiflog.FindAsync(data);
                dataContext.read_at = request.data.Attributes.Read_at;
                await konteks.SaveChangesAsync();
            }

            
            return new Dto
            {
                message = "notification updated",
                success = true
            };


        }
    }
}
