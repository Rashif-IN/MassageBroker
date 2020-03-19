
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace user_handler.UseCase.User.Query.Get
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
            var result = await konteks.user.FirstOrDefaultAsync(X => X.id == request.Id);
            if (result != null)
            {
                return new Dto
                {
                    message = "user retrieved",
                    success = true,
                    Data = result
                };
            }
            else { return null; }
        }
    }
}
