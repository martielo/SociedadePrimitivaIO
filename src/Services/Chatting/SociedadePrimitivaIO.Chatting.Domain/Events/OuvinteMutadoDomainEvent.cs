using NetDevPack.Messaging;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;

namespace SociedadePrimitivaIO.Chatting.Domain.Events
{
    public class OuvinteMutadoDomainEvent : DomainEvent
    {
        public OuvinteMutado OuvinteMutado { get; }

        public OuvinteMutadoDomainEvent(Guid aggregateId, OuvinteMutado ouvinteMutado) : base(aggregateId)
        {
            OuvinteMutado = ouvinteMutado;
        }
    }
}
