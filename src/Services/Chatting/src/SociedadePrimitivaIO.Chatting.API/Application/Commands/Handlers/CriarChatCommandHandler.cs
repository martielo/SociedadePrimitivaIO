using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;

namespace SociedadePrimitivaIO.Chatting.API.Application.Commands.Handlers
{
    public class CriarChatCommandHandler
        : CommandHandler,
            IRequestHandler<CriarChatCommand, ValidationResult>
    {
        private readonly IChatRepository _chatRepository;

        public CriarChatCommandHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<ValidationResult> Handle(
            CriarChatCommand request,
            CancellationToken cancellationToken
        )
        {
            var chat = new Chat(request.Nome, Guid.NewGuid());
            await _chatRepository.Adicionar(chat);

            return await Commit(_chatRepository.UnitOfWork);
        }
    }
}
