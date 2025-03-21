using MongoDB.Bson;

namespace PixelWorldsServer.Server.Event;

public interface IPacketHandler
{
    Task Invoke(EventContext context, BsonDocument document);
}
