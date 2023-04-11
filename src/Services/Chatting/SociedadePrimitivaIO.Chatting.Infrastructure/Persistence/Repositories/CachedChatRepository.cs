using NetDevPack.Data;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;
using System.Text.Json;

namespace SociedadePrimitivaIO.Chatting.Infrastructure.Persistence.Repositories
{
    public class CachedChatRepository : IChatRepository
    {
        private readonly ChatRepository _chatRepository;
        private readonly RedisContext _redisContext;

        public CachedChatRepository(ChatRepository chatRepository, RedisContext redisContext)
        {
            _chatRepository = chatRepository;
            _redisContext = redisContext;
        }

        public IUnitOfWork UnitOfWork => _chatRepository.UnitOfWork;

        public async Task<Chat> ObterPorId(Guid id)
        {
            string key = $"chat-{id}";

            var cachedChat = await _redisContext.Database.StringGetAsync(key);

            Chat chat;
            if (cachedChat.IsNullOrEmpty)
            {
                chat = await _chatRepository.ObterPorId(id);
                if (chat == null) return chat;

                await _redisContext.Add(key, JsonSerializer.Serialize(chat), expiry: TimeSpan.FromMinutes(10), commit: true);

                return chat;
            }

            return JsonSerializer.Deserialize<Chat>(cachedChat);

        }

        public async Task Adicionar(Chat chat)
        {
            await _chatRepository.Adicionar(chat);
        }

        public void Atualizar(Chat chat)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
