namespace SociedadePrimitivaIO.Chatting.BackgroundTasks.Models
{
    public class Mensagem
    {
        public Guid ChatId { get; set; }
        public Guid OuvinteId { get; set; }
        public string Conteudo { get; set; }
    }
}
