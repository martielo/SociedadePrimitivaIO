using NetDevPack.Messaging;

namespace SociedadePrimitivaIO.Chatting.Domain.Events
{
    public class OuvinteDesmutadoDomainEvent : DomainEvent
    {
        public Guid OuvinteId { get; }

        public OuvinteDesmutadoDomainEvent(Guid aggregateId, Guid ouvinteId) : base(aggregateId)
        {
            OuvinteId = ouvinteId;
        }
    }
}
