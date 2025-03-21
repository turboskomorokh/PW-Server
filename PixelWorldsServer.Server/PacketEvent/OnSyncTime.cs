using MongoDB.Bson;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.SYNC_TIME_KEY)]
public class OnSyncTime : IPacketHandler
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
        // unnecessary logging

        context.Player.SendPacket(
            new SyncTimeResponse()
            {
                ID = NetStrings.SYNC_TIME_KEY,
                SyncTime = DateTime.UtcNow.Ticks,
                ServerSleep = 30,
            }
        );

        return Task.CompletedTask;
    }
}
