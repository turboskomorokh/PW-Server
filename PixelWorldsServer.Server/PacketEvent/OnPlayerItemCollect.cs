using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Constants;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.COLLECT_KEY)]

public class OnPlayerItemCollect : IPacketHandler
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
      CollectRequest request = BsonSerializer.Deserialize<CollectRequest>(document);

        var world = context.World;
        if (world is null)
        {
            return Task.CompletedTask;
        }

        // very inefficient, prob better to use dictionary
        var collectable = world.Collectables.FirstOrDefault(x => x.Id == request.CollectableId);
        if (collectable is null)
        {
            return Task.CompletedTask;
        }

        if (!context.Player.CanFitItem(collectable.BlockType, collectable.InventoryItemType, collectable.Amount))
        {
            return Task.CompletedTask;
        }

        world.Collectables.Remove(collectable);

        if (collectable.IsGem)
        {
            switch (collectable.GemType)
            {
                case GemType.Gem1:
                {
                    context.Player.Gems += collectable.Amount * ConfigData.Gem1Value;
                    break;
                }

                case GemType.Gem2:
                {
                    context.Player.Gems += collectable.Amount * ConfigData.Gem2Value;
                    break;
                }

                case GemType.Gem3:
                {
                    context.Player.Gems += collectable.Amount * ConfigData.Gem3Value;
                    break;
                }

                case GemType.Gem4:
                {
                    context.Player.Gems += collectable.Amount * ConfigData.Gem4Value;
                    break;
                }

                case GemType.Gem5:
                {
                    context.Player.Gems += collectable.Amount * ConfigData.Gem5Value;
                    break;
                }
            }
        }
        else
        {
            context.Player.AddItem(collectable.BlockType, collectable.InventoryItemType, collectable.Amount);
        }

        context.Player.SendPacket(new CollectResponse()
        {
            ID = NetStrings.COLLECT_KEY,
            IsGem = collectable.IsGem,
            Amount = collectable.Amount,
            GemType = collectable.GemType,
            BlockType = collectable.BlockType,
            PositionX = collectable.Pos.X,
            PositionY = collectable.Pos.Y,
            CollectableId = request.CollectableId,
            InventoryType = collectable.InventoryItemType,
        });

        var response = new RemoveCollectResponse()
        {
            ID = NetStrings.REMOVE_COLLECT_KEY,
            CollectableId = request.CollectableId,
        };

        foreach (var (_, player) in world.Players)
        {
            player.SendPacket(response);
        }

        return Task.CompletedTask;
    }
}