using MassTransit;

namespace SociedadePrimitivaIO.Chatting.API.Configuration
{
    public static class MassTransitConfig
    {
        public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var aaaa = configuration.GetConnectionString("rabbitmq", binding: "rabbit");
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("rabbitmq", binding: "rabbit"));
                    cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("dev", false));
                    cfg.UseMessageRetry(retry => { retry.Interval(3, TimeSpan.FromSeconds(5)); });
                });
            });

            return services;
        }
    }
}