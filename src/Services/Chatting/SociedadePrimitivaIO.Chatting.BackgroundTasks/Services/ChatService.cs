using Hangfire;
using SociedadePrimitivaIO.Chatting.BackgroundTasks.Services.Jobs;

namespace SociedadePrimitivaIO.Chatting.BackgroundTasks.Services
{
    public class ChatService
    {
        private readonly ChatJob _chatJob;
        private readonly SincronizacaoMensagemJob _sincronizacaoMensagemJob;

        public ChatService(ChatJob chatJob, SincronizacaoMensagemJob sincronizacaoMensagemJob)
        {
            _chatJob = chatJob;
            _sincronizacaoMensagemJob = sincronizacaoMensagemJob;
        }

        public async Task PersistirMensagens(List<Guid> mensagens)
        {
            BackgroundJob.Enqueue(() => _sincronizacaoMensagemJob.AdicionarMensagens(mensagens));
            await Task.CompletedTask;
        }

        public void DesmutarOuvinte(Guid chatId, Guid ouvinteId, TimeSpan tempoParaDesmutar)
        {

        }

    }
}
