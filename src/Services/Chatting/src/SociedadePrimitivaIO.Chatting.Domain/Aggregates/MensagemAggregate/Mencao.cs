using NetDevPack.Domain;

namespace SociedadePrimitivaIO.Chatting.Domain.Aggregates.MensagemAggregate
{
    public class Mencao : ValueObject
    {
        public Guid OuvinteId { get; }
        public string NomeUsuarioOuvinte { get; }

        public Mencao(Guid ouvinteId, string nomeUsuarioOuvinte)
        {
            OuvinteId = ouvinteId;
            NomeUsuarioOuvinte = nomeUsuarioOuvinte;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return OuvinteId;
        }
    }
}
