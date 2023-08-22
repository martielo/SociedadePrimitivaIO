using Microsoft.AspNetCore.Mvc;
using NetDevPack.Mediator;
using SociedadePrimitivaIO.Chatting.API.Application.Commands;

namespace SociedadePrimitivaIO.Chatting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;
        public ChatController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }
        
        [HttpPost]
        public async Task<ActionResult> CriarChat([FromBody] CriarChatCommand command)
        {
            await _mediatorHandler.SendCommand(command);
            return Ok();
        }
    }
}
