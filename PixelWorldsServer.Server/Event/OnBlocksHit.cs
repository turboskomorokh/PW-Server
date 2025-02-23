using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Constants;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[Event(NetStrings.HIT_BLOCK_KEY)]
public class OnHitBlock : IEvent
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
        HitBlockRequest request = BsonSerializer.Deserialize<HitBlockRequest>(document);
        Console.WriteLine("[EH] Triggered OnHitBlock");
        var world = context.World;
        if (world is null)
        {
            throw new Exception("Not in world");
        }

        var block = world.GetBlock(request.X, request.Y);
        if (block.BlockType == BlockType.None)
        {
            return Task.CompletedTask;
        }

        if (block.BlockType == BlockType.Tree)
        {
            var seed = world.GetSeed(request.X, request.Y);
            if (seed is null)
            {
                // what the fuck
                throw new Exception($"No seed in {request.X},{request.Y}");
            }

            if (DateTime.UtcNow >= seed.GrowthEndTime)
            {
                block.HitsRequired = 0;
            }
        }
        else
        {
            if ((DateTime.UtcNow - block.LastHitTime).TotalMilliseconds >= ConfigData.ReviveBlockTime * 1000)
            {
                block.HitsRequired = ConfigData.BlockHitPoints[(int)block.BlockType];
            }
        }

        block.HitsRequired -= 200;
        block.LastHitTime = DateTime.UtcNow;

        if (world.Players.Count > 1)
        {
            var hitResponse = new HitBlockResponse()
            {
                X = request.X,
                Y = request.Y,
                ID = NetStrings.HIT_BLOCK_KEY,
                PlayerId = context.Player.Id,
                TopArmBlockType = BlockType.None,
            };

            foreach (var (id, player) in world.Players)
            {
                if (id != context.Player.Id)
                {
                    player.SendPacket(hitResponse);
                }
            }
        }

        if (block.HitsRequired <= 0)
        {
            var response = new DestroyBlockResponse()
            {
                ID = NetStrings.DESTROY_BLOCK_KEY,
                X = request.X,
                Y = request.Y,
                PlayerId = context.Player.Id,
                BlockDestroyedBlockType = block.BlockType
            };

            foreach (var (_, player) in world.Players)
            {
                player.SendPacket(response);
            }

            var collectables = world.RandomizeCollectablesForDestroyedBlock(new Vector2i(request.X, request.Y), block.BlockType);
            if (collectables is not null)
            {
                foreach (var collectable in collectables)
                {
                    var collectResponse = new CollectResponse()
                    {
                        ID = NetStrings.NEW_COLLECTABLE_KEY,
                        IsGem = collectable.IsGem,
                        Amount = collectable.Amount,
                        GemType = collectable.GemType,
                        BlockType = collectable.BlockType,
                        PositionX = collectable.Pos.X,
                        PositionY = collectable.Pos.Y,
                        CollectableId = collectable.Id,
                        InventoryType = collectable.InventoryItemType,
                    };

                    foreach (var (_, player) in world.Players)
                    {
                        player.SendPacket(collectResponse);
                    }
                }
            }

            block.BlockType = BlockType.None;
        }

        return Task.CompletedTask;
    }

}

[Event(NetStrings.HIT_BLOCK_BACKGROUND_KEY)]
public class OnHitBlockBackground : IEvent
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
        HitBlockRequest request = BsonSerializer.Deserialize<HitBlockRequest>(document);
        Console.WriteLine("[EH] Triggered OnHitBlockBackground");
        var world = context.World;
        if (world is null)
        {
            throw new Exception("Not in world");
        }

        var background = world.GetBackground(request.X, request.Y);
        if (background.BlockType == BlockType.None)
        {
            return Task.CompletedTask;
        }

        if ((DateTime.UtcNow - background.LastHitTime).TotalMilliseconds >= ConfigData.ReviveBlockTime * 1000)
        {
            background.HitsRequired = ConfigData.BlockHitPoints[(int)background.BlockType];
        }

        background.HitsRequired -= 200;
        background.LastHitTime = DateTime.UtcNow;

        if (world.Players.Count > 1)
        {
            var hitResponse = new HitBlockResponse()
            {
                X = request.X,
                Y = request.Y,
                ID = NetStrings.HIT_BLOCK_BACKGROUND_KEY,
                PlayerId = context.Player.Id,
                TopArmBlockType = BlockType.None,
            };

            foreach (var (id, player) in world.Players)
            {
                if (id != context.Player.Id)
                {
                    player.SendPacket(hitResponse);
                }
            }
        }

        if (background.HitsRequired <= 0)
        {
            var response = new DestroyBlockResponse()
            {
                ID = NetStrings.DESTROY_BLOCK_KEY,
                X = request.X,
                Y = request.Y,
                PlayerId = context.Player.Id,
                BlockDestroyedBlockType = background.BlockType
            };

            foreach (var (_, player) in world.Players)
            {
                player.SendPacket(response);
            }

            var collectables = world.RandomizeCollectablesForDestroyedBlock(new Vector2i(request.X, request.Y), background.BlockType);
            if (collectables is not null)
            {
                foreach (var collectable in collectables)
                {
                    var collectResponse = new CollectResponse()
                    {
                        ID = NetStrings.NEW_COLLECTABLE_KEY,
                        IsGem = collectable.IsGem,
                        Amount = collectable.Amount,
                        GemType = collectable.GemType,
                        BlockType = collectable.BlockType,
                        PositionX = collectable.Pos.X,
                        PositionY = collectable.Pos.Y,
                        CollectableId = collectable.Id,
                        InventoryType = collectable.InventoryItemType,
                    };

                    foreach (var (_, player) in world.Players)
                    {
                        player.SendPacket(collectResponse);
                    }
                }
            }

            background.BlockType = BlockType.None;
        }

        return Task.CompletedTask;
    }
}