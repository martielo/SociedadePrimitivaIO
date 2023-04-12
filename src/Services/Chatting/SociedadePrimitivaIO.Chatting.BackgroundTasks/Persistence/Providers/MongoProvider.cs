using MongoDB.Driver;

namespace SociedadePrimitivaIO.Chatting.BackgroundTasks.Persistence.Providers
{
    public class MongoProvider
    {
        private readonly IClientSessionHandle _session;
        private readonly IMongoDatabase _dataBase;

        public MongoProvider(
            IMongoClient mongoClient,
            IClientSessionHandle session,
            IConfiguration configuration
        )
        {
            _session = session;
            _dataBase = mongoClient.GetDatabase(configuration["MongoSettings:DatabaseName"]);
        }

        public async Task InserirVarios<TDocument>(IEnumerable<TDocument> documents)
        {
            var collection = _dataBase.GetCollection<TDocument>(typeof(TDocument).Name);
            await collection.InsertManyAsync(documents);
        }
    }
}