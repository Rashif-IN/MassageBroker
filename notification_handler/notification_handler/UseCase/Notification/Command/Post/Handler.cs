using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using notification_handler.Model;

namespace notification_handler.UseCase.Notification.Command.Post
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
            var listNotif = konteks.notif.ToList();

            var dataNotif = new notif_model()
            {
                message=request.data.Attributes.Message,
                title=request.data.Attributes.Title
            };

            if (!listNotif.Any(x => x.title == request.data.Attributes.Title))
            {
                konteks.notif.Add(dataNotif);
            }
            konteks.SaveChanges();

            var Notif = konteks.notif.First(x => x.title == request.data.Attributes.Title);

            foreach (var x in request.data.Attributes.Target) //overhere
            {
                konteks.Add(new notif_logs_model
                {
                    notification_id = Notif.id,
                    type = request.data.Attributes.Type,
                    from = request.data.Attributes.From,
                    target = x.Id,
                    email_destination = x.Email_destination
                });
                sendmail("rydrydx@ujygu.io", x.Email_destination, request.data.Attributes.Title, request.data.Attributes.Message);
            }

            konteks.SaveChanges();

            return new Dto
            {
                message = "notification posted",
                success = true
            };
        }
        public void sendmail (string emailTo, string emailFrom, string title, string msg )
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("ccdced0fc36ee1", "215162738c26d0"),
                EnableSsl = true
            };
            client.Send(emailFrom, emailTo, title, msg);
            Console.WriteLine("Sent");
        }
    }
}
