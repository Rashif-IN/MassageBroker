
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace user_handler.UseCase.User.Query.GetAll
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
            var result = await konteks.user.ToListAsync();
            return new Dto
            {
                message = "user retrieved",
                success = true,
                Data = result
            };
        }
    }
}
