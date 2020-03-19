using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace user_handler.UseCase.User.Command.Delete
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
            var userdata = await konteks.user.FindAsync(request.Id);
            if(userdata == null)
            { return null; }
            else
            {
                konteks.user.Remove(userdata);
                await konteks.SaveChangesAsync(cancellationToken);
                return new Dto
                {
                    message = "user removed",
                    success = true
                };
            }
            
        }
    }
}

