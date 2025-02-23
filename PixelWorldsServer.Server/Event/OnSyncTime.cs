using MongoDB.Bson;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;
using PixelWorldsServer.Server.Event;

namespace PixelWorldsServer.Server.Event;

[Event(NetStrings.SYNC_TIME_KEY)]
public class OnSyncTime : IEvent
{
  public Task Invoke(EventContext context, BsonDocument document)
  {
    Console.WriteLine("[EH] Triggered OnSyncTime");
    context.Player.SendPacket(new SyncTimeResponse()
    {
      ID = NetStrings.SYNC_TIME_KEY,
      SyncTime = DateTime.UtcNow.Ticks,
      ServerSleep = 30
    });

    return Task.CompletedTask;
  }
}