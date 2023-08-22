using AutoFixture;
using Moq;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.OuvinteAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Exceptions;
using SociedadePrimitivaIO.Chatting.Domain.Services;

namespace SociedadePrimitivaIO.Chatting.UnitTests.Domain.Services
{
    public class ModeracaoChatServiceTest
    {
        private readonly Mock<IChatRepository> _chatRepository;
        private readonly Mock<IOuvinteRepository> _ouvinteRepository;

        public ModeracaoChatServiceTest()
        {
            _chatRepository = new Mock<IChatRepository>();
            _ouvinteRepository = new Mock<IOuvinteRepository>();
        }

        [Fact]
        public async Task MutarOuvinte_OuvinteSolicitanteNaoEhModerador_ThrowsOuvinteNaoEhModeradorException()
        {
            // Arrange
            var fakeOuvinte = new Ouvinte(Guid.NewGuid(), "Fake ouvinte");
            var fakeChat = new Chat("Fake chat", Guid.NewGuid());
            var fakeOuvinteParaSerMutado = new Ouvinte(Guid.NewGuid(), "Ouvinte para mutar");

            var moderacaoChatService = new ModeracaoChatService(
                _ouvinteRepository.Object,
                _chatRepository.Object
            );

            // Act - Assert
            await Assert.ThrowsAsync<OuvinteNaoEhModeradorException>(
                () =>
                    moderacaoChatService.MutarOuvinte(
                        fakeChat,
                        fakeOuvinteParaSerMutado,
                        fakeOuvinte,
                        It.IsAny<TimeSpan>(),
                        It.IsAny<string>()
                    )
            );
        }
    }
}
