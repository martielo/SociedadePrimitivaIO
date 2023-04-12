using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SociedadePrimitivaIO.Chatting.BackgroundTasks.Persistence.Models
{
    public class Mensagem
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid ChatId { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid OuvinteId { get; set; }
        public string Conteudo { get; set; }
    }
}
