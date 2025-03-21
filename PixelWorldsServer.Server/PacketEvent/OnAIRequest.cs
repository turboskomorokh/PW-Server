using MongoDB.Bson;
using PixelWorldsServer.Protocol.Packet;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.REQUEST_AI_ENEMY_KEY)]
public class OnRequestAIEnemy : IPacketHandler
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
        context.Player.SendPacket(new PacketBase() { ID = NetStrings.REQUEST_AI_ENEMY_KEY });

        return Task.CompletedTask;
    }
}

[PacketHandler(NetStrings.REQUEST_AI_PETS_KEY)]
public class OnRequestAIPets : IPacketHandler
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
        context.Player.SendPacket(new PacketBase() { ID = NetStrings.REQUEST_AI_PETS_KEY });

        return Task.CompletedTask;
    }
}
