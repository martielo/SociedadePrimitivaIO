using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.OuvinteAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Exceptions;

namespace SociedadePrimitivaIO.Chatting.Domain.Services
{
    public class ModeracaoChatService
    {
        private readonly IOuvinteRepository _ouvinteRepository;
        private readonly IChatRepository _chatRepository;

        public ModeracaoChatService(IOuvinteRepository ouvinteRepository, IChatRepository chatRepository)
        {
            _ouvinteRepository = ouvinteRepository;
            _chatRepository = chatRepository;
        }

        public async Task MutarOuvinte(Guid chatId, Guid ouvinteId, Guid moderadorId)
        {
            var chat = await _chatRepository.ObterPorId(chatId);
            if (chat == null)
            {
                throw new ChatNaoEncontradoException(chatId);
            }
        }

        public async Task RebaixarModerador(Guid podcastId, Guid apresentadorId, Guid moderadorId)
        {
            var apresentador = await _ouvinteRepository.ObterPorId(apresentadorId) ?? throw new OuvinteNaoEncontradoException(apresentadorId);

            if (!apresentador.EhApresentador(podcastId))
            {
                //throw
            }

            var moderador = await _ouvinteRepository.ObterPorId(moderadorId) ?? throw new OuvinteNaoEncontradoException(moderadorId);

            if (!moderador.EhModerador(podcastId))
            {
                throw new OuvinteNaoEhModeradorException(moderador.Id);
            }

            moderador.MudarCargo(podcastId, Cargo.Ouvinte);

        }
    }
}
