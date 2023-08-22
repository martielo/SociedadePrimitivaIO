using NetDevPack.Domain;

namespace SociedadePrimitivaIO.Chatting.Domain.Exceptions
{
    public class OuvinteNaoEstaMutadoException : DomainException
    {
        public Guid OuvinteId { get; }

        public OuvinteNaoEstaMutadoException(Guid ouvinteId)
            : base($"Ouvinte de id: '{ouvinteId}' não está mutado.")
        {
            OuvinteId = ouvinteId;
        }
    }
}
