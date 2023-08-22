using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.OuvinteAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Services;

namespace SociedadePrimitivaIO.Chatting.API.Application.Commands.Handlers
{
    public class MutarOuvinteCommandHandler
        : CommandHandler,
            IRequestHandler<MutarOuvinteCommand, ValidationResult>
    {
        private readonly ModeracaoChatService _moderacaoChatService;
        private readonly IChatRepository _chatRepository;
        private readonly IOuvinteRepository _ouvinteRepository;

        public MutarOuvinteCommandHandler(
            ModeracaoChatService moderacaoChatService,
            IChatRepository chatRepository,
            IOuvinteRepository ouvinteRepository
        )
        {
            _moderacaoChatService = moderacaoChatService;
            _chatRepository = chatRepository;
            _ouvinteRepository = ouvinteRepository;
        }

        public async Task<ValidationResult> Handle(
            MutarOuvinteCommand request,
            CancellationToken cancellationToken
        )
        {
            var chat = await _chatRepository.ObterPorId(request.ChatId);
            if (chat == null)
            {
                AddError("Chat não encontrado");
            }
            var ouvinte = await _ouvinteRepository.ObterPorId(request.OuvinteId);
            if (ouvinte == null)
            {
                AddError("Ouvinte não encontrado");
            }
            var moderador = await _ouvinteRepository.ObterPorId(request.ModeradorId);
            if (moderador == null)
            {
                AddError("Ouvinte/Moderador não encontrado");
            }

            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }

            await _moderacaoChatService.MutarOuvinte(
                chat,
                ouvinte,
                moderador,
                request.Duracao,
                request.Razao
            );

            await Commit(_chatRepository.UnitOfWork);

            return ValidationResult;
        }
    }
}
