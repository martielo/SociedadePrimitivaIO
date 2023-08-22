using System.Text.Json;
using NetDevPack.Data;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.MensagemAggregate;

namespace SociedadePrimitivaIO.Chatting.Infrastructure.Persistence.Repositories
{
    public class CachedMensagemRepository : IMensagemRepository
    {
        private readonly RedisContext _redisContext;

        public CachedMensagemRepository(RedisContext redisContext)
        {
            _redisContext = redisContext;
        }

        public IUnitOfWork UnitOfWork => _redisContext;

        public async Task Adicionar(Mensagem mensagem)
        {
            string key = $"mensagem-{mensagem.Id}";
            await _redisContext.Add(key, JsonSerializer.Serialize(mensagem), expiry: TimeSpan.FromMinutes(30));
        }

        public void Atualizar(Mensagem mensagem)
        {
            throw new NotImplementedException();
        }

        public Task<Mensagem> ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
