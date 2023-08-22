using NetDevPack.Data;

namespace SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate
{
    public interface IChatRepository : IRepository<Chat>
    {
        Task<Chat> ObterPorId(Guid id);
        Task Adicionar(Chat chat);
        void Atualizar(Chat chat);
    }
}
