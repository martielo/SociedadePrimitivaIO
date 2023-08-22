using NetDevPack.Domain;
using SociedadePrimitivaIO.Chatting.Domain.Events;

namespace SociedadePrimitivaIO.Chatting.Domain.Aggregates.MensagemAggregate
{
    public class Mensagem : Entity, IAggregateRoot
    {
        public Guid ChatId { get; private set; }
        public Guid OuvinteId { get; private set; }
        public string Conteudo { get; private set; }
        public Mencao Mencao { get; private set; }
        public IReadOnlyCollection<Emoji> Emojis => _emojis.AsReadOnly();

        private readonly List<Emoji> _emojis;

        public Mensagem(Guid chatId, string texto) : this()
        {
            ChatId = chatId;
            Conteudo = texto;
        }

        private Mensagem()
        {
            _emojis = new List<Emoji>();
        }

        public static Mensagem Criar(Guid chatId, string texto)
        {
            var mensagem = new Mensagem(chatId, texto);
            mensagem.AddDomainEvent(new MensagemCriadaDomainEvent(mensagem.Id, mensagem));
            return mensagem;

        }

        public bool ContemEmoji() => _emojis.Any();

    }
}
