using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.MY_POSITION_KEY)]
public class OnMyPosition : IPacketHandler {

    public Task Invoke(EventContext context, BsonDocument document)
    {
      MyPosRequest request = BsonSerializer.Deserialize<MyPosRequest>(document);

        var world = context.World;
        if (world is null)
        {
            throw new Exception("Not in world");
        }

        if (request.X == 0 && request.Y == 0)
        {
            return Task.CompletedTask;
        }

        context.Player.Teleport = request.Teleport;
        context.Player.Direction = request.Direction;
        context.Player.Animation = request.Animation;
        context.Player.Position.X = request.X;
        context.Player.Position.Y = request.Y;

        if (world.Players.Count > 1)
        {
            var response = new MyPosResponse()
            {
                X = request.X,
                Y = request.Y,
                ID = NetStrings.MY_POSITION_KEY,
                Teleport = request.Teleport,
                PlayerId = context.Player.Id,
                Direction = request.Direction,
                Animation = request.Animation
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