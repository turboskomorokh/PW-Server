using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Constants;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Utils;
using PixelWorldsServer.Server.Event;

[Event(NetStrings.CHARACTER_CREATED_KEY)]
public class OnPlayerCreatedCharacter : IEvent
{

  public Task Invoke(EventContext context, BsonDocument document)
  {
    CharacterCreatedRequest request = BsonSerializer.Deserialize<CharacterCreatedRequest>(document);
    Console.WriteLine("[EH] Triggered OnCharacterCreated");
    if (context.Player.TutorialState != TutorialState.NotStarted)
    {
      throw new Exception("Nice try");
    }

    context.Player.Skin = request.SkinColorIndex;
    context.Player.Gender = request.Gender;
    context.Player.CountryCode = request.CountryCode;

    context.Player.AddItem(BlockType.GemSoil, InventoryItemType.Seed, 1);
    context.Player.AddItem(BlockType.GemSoil, InventoryItemType.Block, 4);
    context.Player.AddItem(BlockType.Fertilizer, InventoryItemType.Block, 1);
    context.Player.AddItem(BlockType.LockWorldNoob, InventoryItemType.Block, 1);
    context.Player.AddItem(BlockType.ConsumableRedScroll, InventoryItemType.Consumable, 1);

    if (context.Player.Gender == Gender.Male)
    {
      context.Player.AddItem(BlockType.JumpsuitMale, InventoryItemType.WearableItem, 1);
      context.Player.AddItem(BlockType.HatJumpsuitMale, InventoryItemType.WearableItem, 1);
    }
    else
    {
      context.Player.AddItem(BlockType.JumpsuitFemale, InventoryItemType.WearableItem, 1);
      context.Player.AddItem(BlockType.HatJumpsuitFemale, InventoryItemType.WearableItem, 1);
    }

    return Task.CompletedTask;
  }
}