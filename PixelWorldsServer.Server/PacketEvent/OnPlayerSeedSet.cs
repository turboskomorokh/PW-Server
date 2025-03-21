using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Constants;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;
namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.SET_SEED_KEY)]
public class OnPlayerSeedSet : IPacketHandler
{
  public Task Invoke(EventContext context, BsonDocument document)
  {
    SetSeedRequest request = BsonSerializer.Deserialize<SetSeedRequest>(document);

    if (context.World is null)
      throw new Exception("Not in world");
    
    var world = context.World;

    var inventoryItemType = request.BlockType == BlockType.Fertilizer ? InventoryItemType.Block : InventoryItemType.Seed;
    if (!context.Player.HasItem(request.BlockType, inventoryItemType))
      return Task.CompletedTask;

    var seed = world.GetSeed(request.X, request.Y);
    var setFertilizer = false;
    if (seed is not null)
    {
      if (request.BlockType == BlockType.Fertilizer)
      {
        if (context.Player.TutorialState == TutorialState.NotStarted)
        {
          seed.GrowthEndTime = DateTime.MinValue;
        }
        else
        {
          seed.GrowthEndTime -= TimeSpan.FromHours(1);
        }
        setFertilizer = true;
      }
      else if (!seed.IsAlreadyCrossBred)
      {
        var resultBlockType = Seeds.GetCrossBreedingResult(new(seed.BlockType, request.BlockType));
        if (resultBlockType != BlockType.None)
        {
          seed = Seeds.GenerateSeedData(resultBlockType, seed.Position, true);
          world.SetSeed(seed.Position.X, seed.Position.Y, seed);
        }
        else
        {
          return Task.CompletedTask;
        }
      }
      else
      {
        return Task.CompletedTask;
      }
    }
    else
    {
      seed = Seeds.GenerateSeedData(request.BlockType, new Vector2i(request.X, request.Y));
      world.SetSeed(request.X, request.Y, seed);
      // now in SetSeed
      // world.SetBlock(request.X, request.Y, BlockType.Tree);
    }

    context.Player.RemoveItem(request.BlockType, inventoryItemType, 1);

    var response = new SetSeedResponse()
    {
      X = seed.Position.X,
      Y = seed.Position.Y,
      ID = NetStrings.SET_SEED_KEY,
      IsMixed = seed.IsAlreadyCrossBred,
      PlayerId = context.Player.Id,
      BlockType = seed.BlockType,
      HarvestGems = seed.HarvestGems,
      HarvestSeeds = seed.HarvestSeeds,
      HarvestBlocks = seed.HarvestBlocks,
      GrowthEndTime = seed.GrowthEndTime.Ticks,
      SetFertilizer = setFertilizer,
      HarvestExtraBlocks = seed.HarvestExtraBlocks,
      GrowthDurationInSeconds = seed.GrowthDurationInSeconds,
    };

    foreach (var (_, player) in world.Players)
    {
      player.SendPacket(response);
    }

    return Task.CompletedTask;
  }
}