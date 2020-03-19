
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace notification_handler.Controllers
{
    [ApiController]
    [Route("notification")]
    public class notif_controller : ControllerBase
    {
        private IMediator meciater;

        public notif_controller(IMediator mediator)
        {
            meciater = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get(string include)
        {
            if(include == "logs")
            {
                var result = new UseCase.Notification_logs.Query.GetAll.Command();
                return Ok(await meciater.Send(result));
            }
            else
            {
                var result = new UseCase.Notification.Query.GetAll.Command();
                return Ok(await meciater.Send(result));
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int ID)
        {
            var result = new UseCase.Notification.Query.Get.Command(ID);
            var wait = await meciater.Send(result);
            return wait != null ? (IActionResult)Ok(wait) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(UseCase.Notification.Command.Post.Command _Data)
        {
            var result = await meciater.Send(_Data); //overhere
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int ID, UseCase.Notification.Command.Put.Command _Data)
        {
            _Data.data.Attributes.Notification_id = ID;
            var result = await meciater.Send(_Data);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int ID)
        {
            var command = new UseCase.Notification.Command.Delete.Command(ID);
            var result = await meciater.Send(command);
            return result != null ? (IActionResult)Ok(new { Message = "success" }) : NotFound(new { Message = "not found" });

        }
    }
}
