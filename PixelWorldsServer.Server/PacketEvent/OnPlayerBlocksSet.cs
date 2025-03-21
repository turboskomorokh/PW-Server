using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Constants;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.SET_BLOCK_KEY)]
public class OnPlayerSetBlock : IPacketHandler
{
  public Task Invoke(EventContext context, BsonDocument document)
  {
    SetBlockRequest request = BsonSerializer.Deserialize<SetBlockRequest>(document);

    var world = context.World;
    if (world is null)
    {
      throw new Exception("Not in world");
    }

    var block = world.GetBlock(request.X, request.Y);
    if (block.BlockType != BlockType.None)
    {
      return Task.CompletedTask;
    }

    if (!context.Player.HasItem(request.BlockType, InventoryItemType.Block))
    {
      return Task.CompletedTask;
    }

    if (ConfigData.BlockInventoryItemType[(int)request.BlockType] != InventoryItemType.Block)
    {
      throw new Exception("Not a block");
    }

    world.SetBlock(request.X, request.Y, request.BlockType);
    context.Player.RemoveItem(request.BlockType, InventoryItemType.Block, 1);

    foreach (var (_, player) in world.Players)
    {
      player.SendPacket(new SetBlockResponse()
      {
        ID = NetStrings.SET_BLOCK_KEY,
        X = request.X,
        Y = request.Y,
        PlayerId = context.Player.Id,
        BlockType = request.BlockType
      });
    }

    return Task.CompletedTask;
  }
}

[PacketHandler(NetStrings.SET_BLOCK_BACKGROUND_KEY)]
public class OnPlayerSetBlockBackground : IPacketHandler
{
  public Task Invoke(EventContext context, BsonDocument document)
  {
    SetBlockRequest request = BsonSerializer.Deserialize<SetBlockRequest>(document);

    var world = context.World;
    if (world is null)
    {
      throw new Exception("Not in world");
    }

    if (!context.Player.HasItem(request.BlockType, InventoryItemType.BlockBackground))
    {
      return Task.CompletedTask;
    }

    if (ConfigData.BlockInventoryItemType[(int)request.BlockType] != InventoryItemType.BlockBackground)
    {
      throw new Exception("Not a background");
    }

    world.SetBlockBackground(request.X, request.Y, request.BlockType);
    context.Player.RemoveItem(request.BlockType, InventoryItemType.BlockBackground, 1);

    var response = new SetBlockResponse()
    {
      ID = NetStrings.SET_BLOCK_BACKGROUND_KEY,
      X = request.X,
      Y = request.Y,
      PlayerId = context.Player.Id,
      BlockType = request.BlockType
    };
    foreach (var (_, player) in world.Players)
    {
      player.SendPacket(response);
    }

    return Task.CompletedTask;
  }
}