using NetDevPack.Messaging;

namespace SociedadePrimitivaIO.Chatting.API.Application.Commands
{
    public class MutarOuvinteCommand : Command
    {
        public Guid ChatId { get; set; }
        public Guid OuvinteId { get; set; }
        public Guid ModeradorId { get; set; }
        public TimeSpan Duracao { get; set; }
        public string Razao { get; set; }
    }
}
