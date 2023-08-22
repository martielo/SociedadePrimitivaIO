using NetDevPack.Domain;

namespace SociedadePrimitivaIO.Chatting.Domain.Exceptions
{
    public class OuvinteNaoEncontradoException : DomainException
    {
        public Guid OuvinteId { get; }

        public OuvinteNaoEncontradoException(Guid ouvinteId) :
            base($"Ouvinte de id: '{ouvinteId}' não encontrado.")
        {
            OuvinteId = ouvinteId;
        }
    }
}
