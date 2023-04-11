using NetDevPack.Domain;

namespace SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate
{
    public class OuvinteAtivo : ValueObject
    {
        public Guid OuvinteId { get; }

        public OuvinteAtivo(Guid ouvinteId)
        {
            OuvinteId = ouvinteId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return OuvinteId;
        }
    }
}
