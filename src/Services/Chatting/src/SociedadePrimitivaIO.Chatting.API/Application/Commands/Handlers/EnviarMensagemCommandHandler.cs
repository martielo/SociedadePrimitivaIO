using FluentValidation.Results;
using MediatR;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.MensagemAggregate;

namespace SociedadePrimitivaIO.Chatting.API.Application.Commands.Handlers
{
    public class EnviarMensagemCommandHandler : CommandHandler,
        IRequestHandler<EnviarMensagemCommand, ValidationResult>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IChatRepository _chatRepository;
        private readonly IMensagemRepository _mensagemRepository;

        public EnviarMensagemCommandHandler(IMediatorHandler mediatorHandler, IChatRepository chatRepository, IMensagemRepository mensagemRepository)
        {
            _mediatorHandler = mediatorHandler;
            _chatRepository = chatRepository;
            _mensagemRepository = mensagemRepository;
        }

        public async Task<ValidationResult> Handle(EnviarMensagemCommand request, CancellationToken cancellationToken)
        {
            var chat = await _chatRepository.ObterPorId(request.ChatId);
            if (chat == null)
            {
                AddError("Chat não encontrado");
            }

            var mensagem = Mensagem.Criar(chat.Id, "Teste mensagem");
            await _mensagemRepository.Adicionar(mensagem);

            await Commit(_mensagemRepository.UnitOfWork);

            if (ValidationResult.IsValid)
            {
                await _mediatorHandler.PublishDomainEvents(mensagem);
            }

            return ValidationResult;
        }
    }
}
