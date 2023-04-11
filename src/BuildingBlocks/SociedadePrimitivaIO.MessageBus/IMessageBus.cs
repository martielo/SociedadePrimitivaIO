using SociedadePrimitivaIO.Core.Messaging;

namespace SociedadePrimitivaIO.MessageBus
{
    public interface IMessageBus
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : IntegrationEvent;
    }
}
