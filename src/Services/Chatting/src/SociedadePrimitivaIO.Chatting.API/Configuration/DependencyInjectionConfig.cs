using MediatR;
using NetDevPack.Mediator;
using SociedadePrimitivaIO.Chatting.API.Application.Commands;
using SociedadePrimitivaIO.Chatting.API.Application.Commands.Handlers;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.MensagemAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Policies;
using SociedadePrimitivaIO.Chatting.Infrastructure.Persistence;
using SociedadePrimitivaIO.Chatting.Infrastructure.Persistence.Repositories;
using SociedadePrimitivaIO.MessageBus;

namespace SociedadePrimitivaIO.Chatting.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddMediatR(
                cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            );
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<
                IRequestHandler<EnviarMensagemCommand, FluentValidation.Results.ValidationResult>,
                EnviarMensagemCommandHandler
            >();

            services.AddScoped<IMessageBus, MessageBus.MessageBus>();

            services.AddScoped<MongoContext>();
            services.AddScoped<RedisContext>();

            services.AddScoped<ChatRepository>();
            services.AddScoped<IChatRepository, CachedChatRepository>();
            services.AddScoped<IMensagemRepository, CachedMensagemRepository>();

            services.AddTransient<ICastigoChatPolicy, CastigoChatPolicy>();

            return services;
        }
    }
}
