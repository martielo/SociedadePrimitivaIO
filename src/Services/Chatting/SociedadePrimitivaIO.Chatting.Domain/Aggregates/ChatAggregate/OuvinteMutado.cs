using NetDevPack.Domain;

namespace SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate
{
    public class OuvinteMutado : ValueObject
    {
        public Guid OuvinteId { get; }
        public TimeSpan Duracao { get; }
        public string Razao { get; }

        public OuvinteMutado(Guid ouvinteId, TimeSpan duracao, string razao)
        {
            OuvinteId = ouvinteId;
            Duracao = duracao;
            Razao = razao;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return OuvinteId;
        }
    }
}
