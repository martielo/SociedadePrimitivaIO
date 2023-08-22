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
        public async Task MutarOuvinte_ChatIdInvalido_ThrowsChatNaoEncontradoException()
        {
            // Arrange
            _chatRepository.Setup(c => c.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(() => null);
            var moderacaoChatService = new ModeracaoChatService(
                _ouvinteRepository.Object,
                _chatRepository.Object
            );

            // Act - Assert
            await Assert.ThrowsAsync<ChatNaoEncontradoException>(
                () =>
                    moderacaoChatService.MutarOuvinte(
                        It.IsAny<Guid>(),
                        It.IsAny<Guid>(),
                        It.IsAny<Guid>(),
                        It.IsAny<TimeSpan>(),
                        It.IsAny<string>()
                    )
            );
        }
    }
}
