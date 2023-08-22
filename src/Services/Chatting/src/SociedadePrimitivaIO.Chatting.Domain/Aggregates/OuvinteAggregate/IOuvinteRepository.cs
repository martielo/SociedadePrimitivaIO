namespace SociedadePrimitivaIO.Chatting.Domain.Aggregates.OuvinteAggregate
{
    public interface IOuvinteRepository
    {
        Task<Ouvinte> ObterPorId(Guid id);
    }
}
