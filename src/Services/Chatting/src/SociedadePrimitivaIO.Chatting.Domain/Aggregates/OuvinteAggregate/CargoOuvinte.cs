using NetDevPack.Domain;

namespace SociedadePrimitivaIO.Chatting.Domain.Aggregates.OuvinteAggregate
{
    public class CargoOuvinte : Entity
    {
        public Guid PodcastId { get; private set; }
        public Cargo Cargo { get; private set; }

        public CargoOuvinte(Guid podcastId, Cargo cargo )
        {
            PodcastId = podcastId;
            Cargo = cargo;
        }

        public void MudarCargo(Cargo cargo)
        {
            Cargo = cargo;
        }
    }
}
