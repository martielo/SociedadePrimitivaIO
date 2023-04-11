using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SociedadePrimitivaIO.Chatting.Infrastructure.Persistence.Maps;

namespace SociedadePrimitivaIO.Chatting.Infrastructure.Persistence
{
    public static class MongoDbMapConfig
    {
        public static void Configure()
        {
#pragma warning disable CS0618 // GuidRepresentationMode until it defaults to V3 in the future
            BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
#pragma warning restore CS0618 // GuidRepresentationMode until it defaults to V3 in the future
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            ChatMap.Configure();
        }
    }
}