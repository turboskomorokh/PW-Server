using MongoDB.Bson;
using PixelWorldsServer.Protocol.Packet;
using PixelWorldsServer.Protocol.Utils;
namespace PixelWorldsServer.Server.Event;

[Event(NetStrings.REQUEST_AI_ENEMY_KEY)]

public class OnRequestAIEnemy : IEvent
{
  public Task Invoke(EventContext context, BsonDocument document)
  {
    Console.WriteLine("[EH] Triggered OnRequestAIEnemy");
    context.Player.SendPacket(new PacketBase()
    {
      ID = NetStrings.REQUEST_AI_ENEMY_KEY
    });

    return Task.CompletedTask;
  }
}
[Event(NetStrings.REQUEST_AI_PETS_KEY)]
public class OnRequestAIPets : IEvent
{
  public Task Invoke(EventContext context, BsonDocument document)
  {
    Console.WriteLine("[EH] Triggered OnRequestAIPets");
    context.Player.SendPacket(new PacketBase()
    {
      ID = NetStrings.REQUEST_AI_PETS_KEY
    });

    return Task.CompletedTask;
  }
}