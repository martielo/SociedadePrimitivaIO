using System.Text.Json.Serialization;
using NetDevPack.Messaging;

namespace SociedadePrimitivaIO.Chatting.API.Application.Commands
{
    public class EnviarMensagemCommand : Command
    {
        public Guid ChatId { get; set; }
    }
}
