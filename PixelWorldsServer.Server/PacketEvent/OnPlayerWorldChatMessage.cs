using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.WORLD_CHAT_MESSAGE_KEY)]
public class OnPlayerWorldChatMessage : IPacketHandler
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
        WorldChatMessageRequest request = BsonSerializer.Deserialize<WorldChatMessageRequest>(
            document
        );

        var world = context.World;
        if (world is null)
        {
            throw new Exception("Not in world");
        }

        if (world.Players.Count > 1)
        {
            var response = new WorldChatMessageResponse()
            {
                ID = NetStrings.WORLD_CHAT_MESSAGE_KEY,
                MessageBinary = new()
                {
                    Time = DateTime.UtcNow,
                    Nick = context.Player.Name,
                    UserId = context.Player.Id,
                    Channel = $"#{world.Name}",
                    MessageChat = request.Message,
                    ChannelIndex = 0,
                },
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
