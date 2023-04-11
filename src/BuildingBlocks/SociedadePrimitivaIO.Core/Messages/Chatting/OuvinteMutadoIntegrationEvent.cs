using SociedadePrimitivaIO.Core.Messaging;

namespace SociedadePrimitivaIO.Core.Messages.Chatting
{
    public class OuvinteMutadoIntegrationEvent : IntegrationEvent
    {
        public Guid OuvinteId { get; }
        public TimeSpan Duracao { get; }

        public OuvinteMutadoIntegrationEvent(Guid ouvinteId, TimeSpan duracao)
        {
            OuvinteId = ouvinteId;
            Duracao = duracao;
        }
    }
}
