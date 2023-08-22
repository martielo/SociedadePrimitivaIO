using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.OuvinteAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Events;
using SociedadePrimitivaIO.Chatting.Domain.Exceptions;

namespace SociedadePrimitivaIO.Chatting.Domain.Services
{
    public class ModeracaoChatService
    {
        private readonly IOuvinteRepository _ouvinteRepository;
        private readonly IChatRepository _chatRepository;

        public ModeracaoChatService(
            IOuvinteRepository ouvinteRepository,
            IChatRepository chatRepository
        )
        {
            _ouvinteRepository = ouvinteRepository;
            _chatRepository = chatRepository;
        }

        public async Task<Chat> MutarOuvinte(
            Guid chatId,
            Guid ouvinteId,
            Guid moderadorId,
            TimeSpan duracao,
            string razao
        )
        {
            var chat =
                await _chatRepository.ObterPorId(chatId)
                ?? throw new ChatNaoEncontradoException(chatId);
            var _ =
                await _ouvinteRepository.ObterPorId(ouvinteId)
                ?? throw new OuvinteNaoEncontradoException(ouvinteId);
            var moderador =
                await _ouvinteRepository.ObterPorId(moderadorId)
                ?? throw new OuvinteNaoEncontradoException(moderadorId);

            if (!moderador.EhModerador(chat.PodcastId))
            {
                throw new OuvinteNaoEhModeradorException(moderador.Id);
            }

            if (chat.OuvinteEstaMutado(ouvinteId))
            {
                throw new OuvinteNaoEstaMutadoException(ouvinteId);
            }

            chat.ChatDeveEstarAtivo();
            var ouvinteMutado = new OuvinteMutado(ouvinteId, duracao, razao);
            chat._ouvintesMutados.Add(ouvinteMutado);
            chat.AddDomainEvent(new OuvinteMutadoDomainEvent(chat.Id, ouvinteMutado));

            return chat;
        }
    }
}
