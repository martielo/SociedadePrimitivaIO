using NetDevPack.Domain;
using NetDevPack.Mediator;

namespace SociedadePrimitivaIO.Chatting.API.Application
{
    public static class MediatorExtensions
    {
        public static async Task PublishDomainEvents<T>(this IMediatorHandler mediator, T aggregate) where T : Entity, IAggregateRoot
        {
            var domainEvents = aggregate.DomainEvents;

            if (domainEvents == null)
            {
                return;
            }

            var tasks = domainEvents
               .Select(async (domainEvent) =>
               {
                   await mediator.PublishEvent(domainEvent);
               });

            await Task.WhenAll(tasks);
            aggregate.ClearDomainEvents();
        }
    }
}
