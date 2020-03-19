
using MediatR;
using user_handler.Model;

namespace user_handler.UseCase.User.Command.Put
{
    public class Command : RequestData<user_model>, IRequest<Dto>
    {
      
    }
}
