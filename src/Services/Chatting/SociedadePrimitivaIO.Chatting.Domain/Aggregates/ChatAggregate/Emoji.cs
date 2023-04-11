using NetDevPack.Domain;

namespace SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate
{
    public class Emoji : ValueObject
    {
        public string Codigo { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Codigo;
        }
    }
}
