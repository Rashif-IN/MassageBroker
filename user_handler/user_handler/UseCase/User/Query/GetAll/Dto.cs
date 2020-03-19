
using System.Collections.Generic;
using user_handler.Model;

namespace user_handler.UseCase.User.Query.GetAll
{
    public class Dto : dto_model
    {
        public List<user_model> Data { get; set; }
    }
}
