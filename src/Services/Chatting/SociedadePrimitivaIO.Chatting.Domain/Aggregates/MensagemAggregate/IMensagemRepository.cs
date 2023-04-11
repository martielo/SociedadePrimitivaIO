using NetDevPack.Data;

namespace SociedadePrimitivaIO.Chatting.Domain.Aggregates.MensagemAggregate
{
    public interface IMensagemRepository : IRepository<Mensagem>
    {
        Task<Mensagem> ObterPorId(Guid id);
        Task Adicionar(Mensagem mensagem);
        void Atualizar(Mensagem mensagem);
    }
}
