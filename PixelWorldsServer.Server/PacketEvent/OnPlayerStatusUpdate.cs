using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.PLAYER_STATUS_ICON_UPDATE)]
public class OnPlayerStatusIconUpdateRequest : IPacketHandler
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
        PlayerStatusIconUpdateRequest request =
            BsonSerializer.Deserialize<PlayerStatusIconUpdateRequest>(document);

        var world = context.World;
        if (world is null)
        {
            throw new Exception("Not in world");
        }

        context.Player.StatusIcon = request.StatusIcon;

        if (world.Players.Count > 1)
        {
            var response = new PlayerStatusIconUpdateResponse()
            {
                ID = NetStrings.PLAYER_STATUS_ICON_UPDATE,
                PlayerId = context.Player.Id,
                StatusIcon = request.StatusIcon,
            };

            foreach (var (id, player) in world.Players)
            {
                if (id != context.Player.Id)
                {
                    player.SendPacket(response);
                }
            }
        }

        return Task.CompletedTask;
    }
}
