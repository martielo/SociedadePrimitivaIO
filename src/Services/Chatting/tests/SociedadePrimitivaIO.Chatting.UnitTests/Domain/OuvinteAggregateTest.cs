using SociedadePrimitivaIO.Chatting.Domain.Aggregates.OuvinteAggregate;

namespace SociedadePrimitivaIO.Chatting.UnitTests.Domain
{
    public class OuvinteAggregateTest
    {
        [Fact]
        public void EhModerador_OuvinteEhModerador_ReturnsTrue()
        {
            // Arrange
            var ouvinte = new Ouvinte(Guid.NewGuid(), "Ouvinte");
            var podcastId = Guid.NewGuid();
            ouvinte.AtribuirCargo(podcastId, Cargo.Moderador);

            // Act - Assert
            Assert.True(ouvinte.EhModerador(podcastId));
        }

        [Fact]
        public void EhModerador_OuvinteNaoEhModerador_ReturnsFalse()
        {
            // Arrange
            var ouvinte = new Ouvinte(Guid.NewGuid(), "Ouvinte");
            var podcastId = Guid.NewGuid();

            // Act - Assert
            Assert.False(ouvinte.EhModerador(podcastId));
        }
    }
}
