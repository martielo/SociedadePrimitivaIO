using NetDevPack.Domain;
using SociedadePrimitivaIO.Chatting.Domain.Events;
using SociedadePrimitivaIO.Chatting.Domain.Policies;

namespace SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate
{
    public class Chat : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }
        public short TamanhoMaximoMensagem { get; private set; }
        public Guid EpisodioId { get; private set; }
        public Guid PodcastId { get; private set; }
        public IReadOnlyCollection<Emoji> EmojisLivres => _emojisLivres.AsReadOnly();
        public IReadOnlyCollection<OuvinteAtivo> OuvintesAtivos => _ouvintesAtivos.AsReadOnly();
        public IReadOnlyCollection<OuvinteMutado> OuvintesMutados => _ouvintesMutados.AsReadOnly();

        private readonly List<Emoji> _emojisLivres;
        private readonly List<OuvinteAtivo> _ouvintesAtivos;
        private readonly List<OuvinteMutado> _ouvintesMutados;

        public Chat(string nome, Guid podcastId)
            : this()
        {
            Nome = nome;
            PodcastId = podcastId;
        }

        private Chat()
        {
            _ouvintesAtivos = new List<OuvinteAtivo>();
            _ouvintesMutados = new List<OuvinteMutado>();
            _emojisLivres = new List<Emoji>();
        }

        public void AtivarChat() => Ativo = true;

        public void EncerrarChat() => Ativo = false;

        public async Task MutarOuvinte(
            Guid ouvinteId,
            Guid moderadorId,
            TimeSpan duracao,
            string razao,
            ICastigoChatPolicy castigoChatPolicy
        )
        {
            if (castigoChatPolicy == null)
            {
                // throw
            }

            await castigoChatPolicy.VerificarSePodeMutarOuvinte(
                this,
                ouvinteId,
                moderadorId,
                duracao
            );
            var ouvinteMutado = new OuvinteMutado(ouvinteId, duracao, razao);
            _ouvintesMutados.Add(ouvinteMutado);
            AddDomainEvent(new OuvinteMutadoDomainEvent(Id, ouvinteMutado));
        }

        public bool OuvinteEstaMutado(Guid ouvinteId) =>
            _ouvintesMutados.Any(o => o.OuvinteId == ouvinteId);

        public void ChatDeveEstarAtivo()
        {
            if (!Ativo)
            {
                // throw
            }
        }

        public void AtribuirOuvinte(Guid ouvinteId)
        {
            _ouvintesAtivos.Add(new OuvinteAtivo(ouvinteId));
        }

        public void AtribuirEpisodio(Guid podcastId, Guid episodioId)
        {
            EpisodioId = episodioId;
        }
    }
}
