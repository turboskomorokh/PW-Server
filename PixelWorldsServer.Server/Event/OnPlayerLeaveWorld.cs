using MongoDB.Bson;
using PixelWorldsServer.Protocol.Packet;
using PixelWorldsServer.Protocol.Utils;
namespace PixelWorldsServer.Server.Event;

[Event(NetStrings.LEAVE_WORLD_KEY)]
public class OnPlayerLeaveWorld : IEvent
{
  public Task Invoke(EventContext context, BsonDocument document)
  {
    Console.WriteLine("[EH] Triggered OnLeaveWorld");
    if (context.World is null)
    {
      throw new Exception("Not in world");
    }

    context.World.RemovePlayer(context.Player);
    context.Player.SendPacket(new PacketBase()
    {
      ID = NetStrings.LEAVE_WORLD_KEY
    });

    return Task.CompletedTask;
  }
}