using MediatR;
using SociedadePrimitivaIO.Chatting.Domain.Events;
using SociedadePrimitivaIO.Core.Messages.Chatting;
using SociedadePrimitivaIO.MessageBus;

namespace SociedadePrimitivaIO.Chatting.API.Application.DomainEventHandlers
{
    public class OuvinteMutadoDomainEventHandler : INotificationHandler<OuvinteMutadoDomainEvent>
    {
        private readonly IMessageBus _bus;

        public OuvinteMutadoDomainEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(OuvinteMutadoDomainEvent @event, CancellationToken cancellationToken)
        {
            await _bus.PublishAsync(new OuvinteMutadoIntegrationEvent(@event.OuvinteMutado.OuvinteId, @event.OuvinteMutado.Duracao), cancellationToken);
        }
    }
}
