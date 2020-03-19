
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace user_handler.UseCase.User.Command.Put
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
            var userdata = konteks.user.Find(request.data.Attributes.id);
            userdata.name = request.data.Attributes.name;
            userdata.username = request.data.Attributes.username;
            userdata.email = request.data.Attributes.email;
            userdata.password = request.data.Attributes.password;
            userdata.address = request.data.Attributes.address;
            
            await konteks.SaveChangesAsync(cancellationToken);
            return new Dto
            {
                message = "user updated",
                success = true
            };
           

        }
    }
}
