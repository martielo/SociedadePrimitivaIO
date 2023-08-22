using NetDevPack.Domain;

namespace SociedadePrimitivaIO.Chatting.Domain.Exceptions
{
    public class OuvinteNaoEhModeradorException : DomainException
    {
        public Guid OuvinteId { get; }

        public OuvinteNaoEhModeradorException(Guid ouvinteId) :
            base($"Ouvinte de id: '{ouvinteId}' não é um moderador.")
        {
            OuvinteId = ouvinteId;
        }
    }
}
