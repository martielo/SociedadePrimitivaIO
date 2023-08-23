using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;

namespace SociedadePrimitivaIO.Chatting.Domain.Policies
{
    public interface ICastigoChatPolicy
    {
        Task VerificarSePodeMutarOuvinte(
            Chat chat,
            Guid ouvinteId,
            Guid ouvinteModeradorId,
            TimeSpan duracao
        );
    }
}
