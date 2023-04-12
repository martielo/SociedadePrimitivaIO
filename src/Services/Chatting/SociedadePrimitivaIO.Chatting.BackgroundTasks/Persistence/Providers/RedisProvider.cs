using StackExchange.Redis;

namespace SociedadePrimitivaIO.Chatting.BackgroundTasks.Persistence.Providers
{
    public class RedisProvider
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _dataBase;

        public RedisProvider(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _dataBase = redis.GetDatabase();
        }

        public async Task<string> ObterStringAsync(string key) => await _dataBase.StringGetAsync(key);
    }
}