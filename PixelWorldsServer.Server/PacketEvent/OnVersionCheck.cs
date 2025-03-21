using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;
namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.VERSION_CHECK_KEY)]
public class OnVersionCheck : IPacketHandler {
  public Task Invoke(EventContext context, BsonDocument document)
    {
        VersionCheckRequest request = BsonSerializer.Deserialize<VersionCheckRequest>(document);

        context.Player.OS = request.OS;
        context.Player.OSType = request.OsType;
        context.Player.SendPacket(new VersionCheckResponse()
        {
            ID = NetStrings.VERSION_CHECK_KEY,
            VersionNumber = 103
        });

        return Task.CompletedTask;
    }
}