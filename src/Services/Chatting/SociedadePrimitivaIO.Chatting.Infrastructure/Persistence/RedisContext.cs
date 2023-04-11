using NetDevPack.Data;
using StackExchange.Redis;

namespace SociedadePrimitivaIO.Chatting.Infrastructure.Persistence
{
    public class RedisContext : IUnitOfWork
    {
        public IConnectionMultiplexer Redis { get; }
        public IDatabase Database { get; }

        private readonly ITransaction _transaction;

        public RedisContext(IConnectionMultiplexer redis)
        {
            Redis = redis;
            Database = redis.GetDatabase();

            _transaction = Database.CreateTransaction();
        }

        public async Task<bool> Commit()
        {
            return await _transaction.ExecuteAsync();
        }

        public async Task Add(string key, string value, TimeSpan? expiry = null, bool commit = false)
        {
            await Task.FromResult(_transaction.StringSetAsync(key, value, expiry));

            if (commit)
            {
                await Commit();
            }
        }

        public async Task Update(string key, string value, TimeSpan? expiry = null, bool commit = false)
        {
            await _transaction.StringSetAsync(key, value, expiry);

            if (commit)
            {
                await Commit();
            }
        }

        public async Task Delete(string key, bool commit = false)
        {
            await _transaction.KeyDeleteAsync(key);
        }
    }
}
