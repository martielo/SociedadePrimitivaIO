using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NetDevPack.Data;

namespace SociedadePrimitivaIO.Chatting.Infrastructure.Persistence
{
    public class MongoContext : IUnitOfWork
    {
        public IClientSessionHandle Session { get; }
        private IMongoDatabase Database { get; }

        private readonly List<Func<Task>> _commands;

        public MongoContext(IMongoClient mongoClient, IClientSessionHandle session, IConfiguration configuration)
        {
            Session = session;
            Database = mongoClient.GetDatabase(configuration["MongoSettings:DatabaseName"]);

            _commands = new List<Func<Task>>();
        }

        public async Task<bool> Commit()
        {
            Session.StartTransaction();
            
            var commandTasks = _commands.Select(c => c());

            await Task.WhenAll(commandTasks);

            await Session.CommitTransactionAsync();

            return _commands.Count > 0;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
