using NetDevPack.Messaging;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.MensagemAggregate;

namespace SociedadePrimitivaIO.Chatting.Domain.Events
{
    public class MensagemCriadaDomainEvent : DomainEvent
    {
        public MensagemCriadaDomainEvent(Guid aggregateId, Mensagem mensagem) : base(aggregateId)
        {
            Mensagem = mensagem;
        }

        public Mensagem Mensagem { get; }
    }
}
