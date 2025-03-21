using MongoDB.Bson;
using PixelWorldsServer.Protocol.Packet;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.PING_KEY)]
public class OnPingEvent : IPacketHandler
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
        context.Player.SendPacket(new PacketBase() { ID = NetStrings.PING_KEY });
        return Task.CompletedTask;
    }
}
