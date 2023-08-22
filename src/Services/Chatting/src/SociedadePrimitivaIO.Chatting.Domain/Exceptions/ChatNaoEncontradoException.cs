using NetDevPack.Domain;

namespace SociedadePrimitivaIO.Chatting.Domain.Exceptions
{
    public class ChatNaoEncontradoException : DomainException
    {
        public Guid ChatId { get; }

        public ChatNaoEncontradoException(Guid chatId) :
            base($"Chat de id: '{chatId}' não encontrado.")
        {
            ChatId = chatId;
        }
    }
}
