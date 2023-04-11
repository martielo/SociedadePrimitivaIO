using MassTransit;
using SociedadePrimitivaIO.Core.Messaging;

namespace SociedadePrimitivaIO.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private readonly IBus _bus;

        public MessageBus(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : IntegrationEvent
        {
            await _bus.Publish(message, cancellationToken);
        }
    }
}
