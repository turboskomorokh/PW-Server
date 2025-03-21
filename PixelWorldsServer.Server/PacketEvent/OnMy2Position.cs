using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.MY2_POSITION_KEY)]
public class OnMy2Position : IPacketHandler
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
        My2PosRequest request = BsonSerializer.Deserialize<My2PosRequest>(document);

        var world = context.World;
        if (world is null)
        {
            throw new Exception("Not in world");
        }

        // TODO: wtf is "mp"

        return Task.CompletedTask;
    }
}
