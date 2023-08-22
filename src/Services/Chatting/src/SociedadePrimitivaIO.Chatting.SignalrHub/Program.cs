using MassTransit;
using SociedadePrimitivaIO.Chatting.SignalrHub.Hubs;
using SociedadePrimitivaIO.Chatting.SignalrHub.IntegrationEvents.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSignalR();


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MensagemCriadaIntegrationEventHandler>();
   x.UsingRabbitMq((context, cfg) =>
   {
       cfg.Host(builder.Configuration.GetConnectionString("RabbitMq"));
       cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("dev-signalr", false));
       cfg.UseMessageRetry(retry => { retry.Interval(3, TimeSpan.FromSeconds(5)); });
   });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();
app.MapHub<ChatHub>("/hub/chathub");

app.UseHttpsRedirection();

app.Run();