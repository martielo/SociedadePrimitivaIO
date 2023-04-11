using SociedadePrimitivaIO.Core.Messaging;

namespace SociedadePrimitivaIO.Core.Messages.Chatting
{
    public class MensagemCriadaIntegrationEvent : IntegrationEvent
    {
        public Guid MensagemId { get; }
        public Guid ChatId { get; }

        public MensagemCriadaIntegrationEvent(Guid mensagemId, Guid chatId)
        {
            MensagemId = mensagemId;
            ChatId = chatId;
        }
    }
}
