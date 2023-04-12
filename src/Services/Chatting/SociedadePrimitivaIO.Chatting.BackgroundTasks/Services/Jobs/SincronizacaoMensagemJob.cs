using System.Text.Json;
using SociedadePrimitivaIO.Chatting.BackgroundTasks.Persistence.Models;
using SociedadePrimitivaIO.Chatting.BackgroundTasks.Persistence.Providers;

namespace SociedadePrimitivaIO.Chatting.BackgroundTasks.Services.Jobs
{
    public class SincronizacaoMensagemJob
    {
        private readonly MongoProvider _mongoProvider;
        private readonly RedisProvider _redisProvider;

        public SincronizacaoMensagemJob(
            MongoProvider mongoProvider,
            RedisProvider redisProvider)
        {
            _mongoProvider = mongoProvider;
            _redisProvider = redisProvider;
        }

        public async Task AdicionarMensagens(List<Guid> mensagemIds)
        {
            string keyPrefix = $"mensagem-";
            var mensagens = new List<Mensagem>();

            foreach (var mensagemId in mensagemIds)
            {
                var mensagem = await _redisProvider.ObterStringAsync($"{keyPrefix}{mensagemId}");
                if (mensagem == null)
                {
                    //log
                    continue;
                }

                var document = JsonSerializer.Deserialize<Mensagem>(mensagem);
                mensagens.Add(document);
            }

            await _mongoProvider.InserirVarios(mensagens);
        }
    }
}