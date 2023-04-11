using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SociedadePrimitivaIO.MessageBus;

namespace SociedadePrimitivaIO.Chatting.SignalrHub.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageBus _bus;
        private readonly static ConnectionMapping<string> _connections = new();

        public ChatHub(IMessageBus bus)
        {
            _bus = bus;
        }

        public override async Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;

            _connections.Add(name, Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, "User");

            await base.OnConnectedAsync();


        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
