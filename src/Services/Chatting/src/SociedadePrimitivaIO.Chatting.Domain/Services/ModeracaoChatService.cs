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
            Chat chat,
            Ouvinte ouvinte,
            Ouvinte moderador,
            TimeSpan duracao,
            string razao
        )
        {
            if (!moderador.EhModerador(chat.PodcastId))
            {
                throw new OuvinteNaoEhModeradorException(moderador.Id);
            }

            if (chat.OuvinteEstaMutado(ouvinte.Id))
            {
                throw new OuvinteNaoEstaMutadoException(ouvinte.Id);
            }

            chat.ChatDeveEstarAtivo();
            var ouvinteMutado = new OuvinteMutado(ouvinte.Id, duracao, razao);
            chat._ouvintesMutados.Add(ouvinteMutado);
            chat.AddDomainEvent(new OuvinteMutadoDomainEvent(chat.Id, ouvinteMutado));

            await _chatRepository.Adicionar(chat);

            return chat;
        }
    }
}
