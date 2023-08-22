using NetDevPack.Domain;

namespace SociedadePrimitivaIO.Chatting.Domain.Aggregates.MensagemAggregate
{
    public class Emoji : ValueObject
    {
        public string Codigo { get; }
        public string Nome { get; }
        public string Url { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Codigo;
            yield return Nome;
            yield return Url;
        }
    }
}
