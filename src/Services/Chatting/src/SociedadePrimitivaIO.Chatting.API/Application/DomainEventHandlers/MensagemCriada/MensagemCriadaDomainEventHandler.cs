using MediatR;
using SociedadePrimitivaIO.Chatting.Domain.Events;
using SociedadePrimitivaIO.Core.Messages.Chatting;
using SociedadePrimitivaIO.MessageBus;

namespace SociedadePrimitivaIO.Chatting.API.Application.DomainEventHandlers.MensagemCriada
{
    public class MensagemCriadaDomainEventHandler : INotificationHandler<MensagemCriadaDomainEvent>
    {
        private readonly IMessageBus _bus;

        public MensagemCriadaDomainEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(MensagemCriadaDomainEvent @event, CancellationToken cancellationToken)
        {
            await _bus.PublishAsync(new MensagemCriadaIntegrationEvent(@event.AggregateId, @event.Mensagem.ChatId), cancellationToken);
        }
    }
}
