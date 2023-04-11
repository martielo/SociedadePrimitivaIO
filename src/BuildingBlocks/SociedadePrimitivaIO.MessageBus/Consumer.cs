using MassTransit;
using SociedadePrimitivaIO.Core.Messaging;

namespace SociedadePrimitivaIO.MessageBus
{
    public abstract class Consumer<T> : IConsumer<T> where T : IntegrationEvent
    {
        public abstract Task Consume(ConsumeContext<T> context);
    }

    public abstract class BatchConsumer<T> : IConsumer<Batch<T>> where T : IntegrationEvent
    {
        public abstract Task Consume(ConsumeContext<Batch<T>> context);
    }
}
