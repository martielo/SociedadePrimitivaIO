using MassTransit;
using SociedadePrimitivaIO.Chatting.BackgroundTasks.IntegrationEvents.Handlers;

namespace SociedadePrimitivaIO.Chatting.BackgroundTasks.Configuration
{
    public static class MassTransitConfig
    {
        public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<MensagemCriadaIntegrationEventHandler, MensagemCriadaIntegrationEventHandlerDefinition>();
                
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("rabbitmq", "rabbit"));
                    cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("dev-hangfire", false));
                    cfg.UseMessageRetry(retry => { retry.Interval(3, TimeSpan.FromSeconds(5)); });
                });
                    
            });
                
            return services;
        }
    }
}