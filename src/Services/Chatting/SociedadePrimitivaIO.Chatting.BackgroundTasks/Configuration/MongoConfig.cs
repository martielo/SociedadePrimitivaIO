using MongoDB.Driver;

namespace SociedadePrimitivaIO.Chatting.BackgroundTasks.Configuration
{
    public static class MongoConfig
    {
        public static IServiceCollection ConfigureMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(c =>
            {
                return new MongoClient(configuration.GetConnectionString("mongodb"));
            });

            services.AddScoped(c => c.GetService<IMongoClient>().StartSession());

            return services;
        }
    }
}