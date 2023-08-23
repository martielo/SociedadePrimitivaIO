using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.OuvinteAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Policies;

namespace SociedadePrimitivaIO.Chatting.API.Application.Commands.Handlers
{
    public class MutarOuvinteCommandHandler
        : CommandHandler,
            IRequestHandler<MutarOuvinteCommand, ValidationResult>
    {
        private readonly ICastigoChatPolicy _castigoChatPolicy;
        private readonly IChatRepository _chatRepository;
        private readonly IOuvinteRepository _ouvinteRepository;

        public MutarOuvinteCommandHandler(
            ICastigoChatPolicy castigoChatPolicy,
            IChatRepository chatRepository,
            IOuvinteRepository ouvinteRepository
        )
        {
            _castigoChatPolicy = castigoChatPolicy;
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

            await chat.MutarOuvinte(
                ouvinte.Id,
                moderador.Id,
                request.Duracao,
                request.Razao,
                _castigoChatPolicy
            );

            await _chatRepository.Adicionar(chat);
            await Commit(_chatRepository.UnitOfWork);

            return ValidationResult;
        }
    }
}
