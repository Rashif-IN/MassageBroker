
using MediatR;

namespace user_handler.UseCase.User.Query.Get
{
    public class Command : IRequest<Dto>
    {
        public int Id { get; set; }
        public Command(int id)
        {
            Id = id;
        }
    }
}
