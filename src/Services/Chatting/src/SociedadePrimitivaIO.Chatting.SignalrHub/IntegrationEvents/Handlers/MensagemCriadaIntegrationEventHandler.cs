using MassTransit;
using Microsoft.AspNetCore.SignalR;
using SociedadePrimitivaIO.Chatting.SignalrHub.Hubs;
using SociedadePrimitivaIO.Core.Messages.Chatting;
using SociedadePrimitivaIO.MessageBus;

namespace SociedadePrimitivaIO.Chatting.SignalrHub.IntegrationEvents.Handlers
{
    public class MensagemCriadaIntegrationEventHandler : Consumer<MensagemCriadaIntegrationEvent>
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public MensagemCriadaIntegrationEventHandler(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public override async Task Consume(ConsumeContext<MensagemCriadaIntegrationEvent> context)
        {
            var @event = context.Message;
            await _hubContext.Clients.Group(@event.ChatId.ToString()).SendAsync("ReceiveMessage", "Teste");

        }
    }
}
