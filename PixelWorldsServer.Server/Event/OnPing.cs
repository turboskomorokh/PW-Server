using MongoDB.Bson;
using PixelWorldsServer.Protocol.Packet;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;
using PixelWorldsServer.Server.Event;

namespace PixelWorldsServer.Server.Event;

[Event(NetStrings.PING_KEY)]

public class OnPingEvent : IEvent
{

  public Task Invoke(EventContext context, BsonDocument document)
  {
    // unnecessary logging
    //Console.WriteLine("[EH] Triggered OnPing");
    context.Player.SendPacket(new PacketBase()
    {
      ID = NetStrings.PING_KEY,
    });
    return Task.CompletedTask;
  }
}