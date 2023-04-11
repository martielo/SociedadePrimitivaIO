using Microsoft.VisualBasic;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.MensagemAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.OuvinteAggregate;

namespace SociedadePrimitivaIO.Chatting.Domain.Services
{
    public class MensagemChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IOuvinteRepository _ouvinteRepository;

        public MensagemChatService(IChatRepository chatRepository, IOuvinteRepository ouvinteRepository)
        {
            _chatRepository = chatRepository;
            _ouvinteRepository = ouvinteRepository;
        }

        public async Task EnviarMensagem(Mensagem mensagem)
        {
            if (mensagem.ContemEmoji())
            {
                await EnviarMensagemComEmojis(mensagem);
            }
        }

        private async Task EnviarMensagemComEmojis(Mensagem mensagem)
        {
            var ouvinte = await _ouvinteRepository.ObterPorId(mensagem.OuvinteId);

            var chat = await _chatRepository.ObterPorId(mensagem.ChatId);
            if (chat == null)
            {
                throw new Exception();
            }

            var emojisMensagem = mensagem.Emojis.DistinctBy(x => x.Codigo);
            var emojisExclusivos = mensagem.Emojis.Where(me => !chat.EmojisLivres.Any(c => c.Codigo == me.Codigo));

            if (emojisExclusivos.Any())
            {

            }

        }
    }
}
