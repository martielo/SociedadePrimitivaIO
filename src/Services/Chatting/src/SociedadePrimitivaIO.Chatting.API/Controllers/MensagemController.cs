using Microsoft.AspNetCore.Mvc;
using NetDevPack.Mediator;
using SociedadePrimitivaIO.Chatting.API.Application.Commands;

namespace SociedadePrimitivaIO.Chatting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensagemController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public MensagemController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        public async Task<ActionResult> EnviarMensagem([FromBody] EnviarMensagemCommand command)
        {
            var aaaa = await _mediatorHandler.SendCommand(command);
            return Ok();
        }
    }
}
