using MongoDB.Bson;

namespace PixelWorldsServer.Server.Event;

public interface IEvent {
  Task Invoke(EventContext context, BsonDocument document);
}