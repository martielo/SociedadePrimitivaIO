using NetDevPack.Data;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;

namespace SociedadePrimitivaIO.Chatting.Infrastructure.Persistence.Repositories
{
    public class ChatRepository : MongoRepository<Chat>, IChatRepository
    {
        public ChatRepository(MongoContext context) : base(context)
        {

        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Chat> ObterPorId(Guid id) => await GetById(id);
        public async Task Adicionar(Chat chat)
        {
            Add(chat);
            await Task.CompletedTask;
        }
        public void Atualizar(Chat chat) => Update(chat);

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
