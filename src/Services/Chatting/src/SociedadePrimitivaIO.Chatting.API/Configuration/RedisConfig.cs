using StackExchange.Redis;

namespace SociedadePrimitivaIO.Chatting.API.Configuration
{
    public static class RedisConfig
    {
        public static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(
                ConfigurationOptions.Parse(configuration.GetConnectionString("redis"))));
            
            return services;
        }
    }
}