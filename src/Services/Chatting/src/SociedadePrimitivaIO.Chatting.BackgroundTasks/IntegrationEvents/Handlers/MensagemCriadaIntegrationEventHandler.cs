using MassTransit;
using SociedadePrimitivaIO.Chatting.BackgroundTasks.Services;
using SociedadePrimitivaIO.Core.Messages.Chatting;
using SociedadePrimitivaIO.MessageBus;

namespace SociedadePrimitivaIO.Chatting.BackgroundTasks.IntegrationEvents.Handlers
{
    public class MensagemCriadaIntegrationEventHandler : BatchConsumer<MensagemCriadaIntegrationEvent>
    {
        private readonly ChatService _chatService;
        private readonly List<Guid> _mensagemIds;

        public MensagemCriadaIntegrationEventHandler(ChatService chatService)
        {
            _chatService = chatService;
            _mensagemIds = new();
        }

        public override async Task Consume(ConsumeContext<Batch<MensagemCriadaIntegrationEvent>> context)
        {
            var messages = context.Message.Select(x => x.Message);

            foreach (var message in messages)
            {
                _mensagemIds.Add(message.MensagemId);
            }

            await _chatService.PersistirMensagens(_mensagemIds);
        }
    }

    public class MensagemCriadaIntegrationEventHandlerDefinition :  
        ConsumerDefinition<MensagemCriadaIntegrationEventHandler>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, 
            IConsumerConfigurator<MensagemCriadaIntegrationEventHandler> consumerConfigurator)
        {
            consumerConfigurator.Options<BatchOptions>(c => 
            {
                c.SetMessageLimit(30);
                c.SetTimeLimit(s: 30);
            });
        }
    }
}