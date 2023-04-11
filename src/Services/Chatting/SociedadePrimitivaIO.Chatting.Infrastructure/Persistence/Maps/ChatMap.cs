using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SociedadePrimitivaIO.Chatting.Domain.Aggregates.ChatAggregate;

namespace SociedadePrimitivaIO.Chatting.Infrastructure.Persistence.Maps
{
    internal class ChatMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Chat>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });
        }
    }
}   