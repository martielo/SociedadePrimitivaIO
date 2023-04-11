using Hangfire;
using SociedadePrimitivaIO.Chatting.BackgroundTasks.Services.Jobs;

namespace SociedadePrimitivaIO.Chatting.BackgroundTasks.Services
{
    public class ChatService
    {
        private readonly ChatJob _chatJob;

        public ChatService(ChatJob chatJob)
        {
            _chatJob = chatJob;
        }

        public async Task PersistirMensagens(List<Guid> mensagens)
        {
            BackgroundJob.Enqueue(() => DoSomeLongOperation(mensagens));
            await Task.CompletedTask;
        }

        public void DesmutarOuvinte(Guid chatId, Guid ouvinteId, TimeSpan tempoParaDesmutar)
        {

        }

        public async Task DoSomeLongOperation(List<Guid> mensagens)
        {
            foreach (var mensagem in mensagens)
            {
                Console.Write($"Mensagems para salvar: {mensagem}");
            }

            await Task.CompletedTask;
        }

    }
}
