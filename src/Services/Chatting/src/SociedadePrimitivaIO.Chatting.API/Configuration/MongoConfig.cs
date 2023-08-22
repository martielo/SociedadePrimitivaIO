using MongoDB.Driver;
using SociedadePrimitivaIO.Chatting.Infrastructure.Persistence;

namespace SociedadePrimitivaIO.Chatting.API.Configuration
{
    public static class MongoConfig
    {
        public static IServiceCollection ConfigureMongo(this IServiceCollection services, IConfiguration configuration)
        {
            MongoDbMapConfig.Configure();
            
            services.AddSingleton<IMongoClient>(c =>
            {
                return new MongoClient(configuration.GetConnectionString("mongodb"));
            });

            services.AddScoped(c => c.GetService<IMongoClient>().StartSession());

            return services;
        }
    }
}