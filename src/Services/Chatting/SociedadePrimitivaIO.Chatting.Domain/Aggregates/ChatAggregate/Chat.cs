using NetDevPack.Domain;
using SociedadePrimitivaIO.Chatting.Domain.Events;

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

        public Chat(string nome)
        {
            Nome = nome;
        }

        private Chat()
        {
            _ouvintesAtivos = new List<OuvinteAtivo>();
            _ouvintesMutados = new List<OuvinteMutado>();
            _emojisLivres = new List<Emoji>();
        }

        public void LigarChat()
        {
            Ativo = true;
        }

        public void EncerrarChat()
        {
            Ativo = false;
        }

        public void MutarOuvinte(Guid ouvinteId, TimeSpan duracao, string razao)
        {
            ChatDeveEstarAtivo();

            var ouvinteSilenciado = new OuvinteMutado(ouvinteId, duracao, razao);
            _ouvintesMutados.Add(ouvinteSilenciado);

            AddDomainEvent(new OuvinteMutadoDomainEvent(Id, ouvinteSilenciado));
        }

        public void DesmutarOuvinte(Guid ouvinteId)
        {
            ChatDeveEstarAtivo();

            var ouvinteMutado = _ouvintesMutados.FirstOrDefault(o => o.OuvinteId == ouvinteId);
            if (ouvinteMutado == null)
            {
                //throw
            }

            _ouvintesMutados.Remove(ouvinteMutado);
            AddDomainEvent(new OuvinteDesmutadoDomainEvent(Id, ouvinteId));
        }

        public void OuvinteDeveEstarDesmutado(Guid ouvinteId)
        {
            bool ouvinteMutado = _ouvintesMutados.Any(o => o.OuvinteId == ouvinteId);

            if (ouvinteMutado)
            {
                //throw
            }
        }

        public void ChatDeveEstarAtivo()
        {
            if (!Ativo)
            {
                //throw
            }

        }

        public void AtribuirOuvinte(Guid ouvinteId)
        {
            _ouvintesAtivos.Add(new OuvinteAtivo(ouvinteId));

        }
    }
}