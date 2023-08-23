using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.OuvinteAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Exceptions;

namespace SociedadePrimitivaIO.Chatting.Domain.Policies
{
    public class CastigoChatPolicy : ICastigoChatPolicy
    {
        private readonly IOuvinteRepository _ouvinteRepository;

        private const int TempoMaximoOuvinteMutadoEmMinutos = 10;

        public CastigoChatPolicy(IOuvinteRepository ouvinteRepository)
        {
            _ouvinteRepository = ouvinteRepository;
        }

        public async Task VerificarSePodeMutarOuvinte(
            Chat chat,
            Guid ouvinteId,
            Guid ouvinteModeradorId,
            TimeSpan duracao
        )
        {
            var ouvinte =
                await _ouvinteRepository.ObterPorId(ouvinteId)
                ?? throw new OuvinteNaoEncontradoException(ouvinteId);

            var moderador =
                await _ouvinteRepository.ObterPorId(ouvinteModeradorId)
                ?? throw new OuvinteNaoEncontradoException(ouvinteModeradorId);

            if (!moderador.EhModerador(chat.PodcastId))
            {
                throw new OuvinteNaoEhModeradorException(moderador.Id);
            }

            if (chat.OuvinteEstaMutado(ouvinte.Id))
            {
                throw new OuvinteNaoEstaMutadoException(ouvinte.Id);
            }

            if (duracao > TimeSpan.FromMinutes(TempoMaximoOuvinteMutadoEmMinutos))
            {
                // throw
            }

            chat.ChatDeveEstarAtivo();
        }
    }
}
