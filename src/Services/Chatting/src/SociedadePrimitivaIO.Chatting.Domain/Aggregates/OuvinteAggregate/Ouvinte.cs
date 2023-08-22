using NetDevPack.Domain;

namespace SociedadePrimitivaIO.Chatting.Domain.Aggregates.OuvinteAggregate
{
    public class Ouvinte : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public IReadOnlyCollection<CargoOuvinte> CargosOuvinte => _cargosOuvinte.AsReadOnly();
        public IReadOnlyCollection<EmojiExclusivo> EmojisExclusivos =>
            _emojisExclusivos.AsReadOnly();

        private readonly List<CargoOuvinte> _cargosOuvinte;
        private readonly List<EmojiExclusivo> _emojisExclusivos;

        public Ouvinte(Guid id, string nome)
            : this()
        {
            Id = id;
            Nome = nome;
        }

        private Ouvinte()
        {
            _cargosOuvinte = new List<CargoOuvinte>();
            _emojisExclusivos = new List<EmojiExclusivo>();
        }

        public bool EhApresentador(Guid podcastId) =>
            ObterCargoOuvinte(podcastId)?.Cargo == Cargo.Apresentador;

        public bool EhModerador(Guid podcastId) =>
            ObterCargoOuvinte(podcastId)?.Cargo == Cargo.Moderador;

        public CargoOuvinte ObterCargoOuvinte(Guid podcastId)
        {
            var cargoOuvinte = _cargosOuvinte.FirstOrDefault(c => c.PodcastId == podcastId);
            if (cargoOuvinte == null)
            {
                return null;
            }

            return cargoOuvinte;
        }

        public void AtribuirCargo(Guid podcastId, Cargo cargo)
        {
            var cargoOuvinte = new CargoOuvinte(podcastId, cargo);
            _cargosOuvinte.Add(cargoOuvinte);
        }

        public void MudarCargo(Guid podcastId, Cargo novoCargo)
        {
            var cargoOuvinte = ObterCargoOuvinte(podcastId);
            cargoOuvinte.MudarCargo(novoCargo);
        }

        public bool PossuiEmoji(string codigoEmoji) =>
            _emojisExclusivos.Any(e => e.Codigo == codigoEmoji);
    }
}
