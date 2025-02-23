using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Constants;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;
namespace PixelWorldsServer.Server.Event;

[Event(NetStrings.BUY_ITEM_PACK_KEY)]
// TODO fix stub
public class OnPlayerBuyItemPack : IEvent
{
  Random random = new Random();
  public Task Invoke(EventContext context, BsonDocument document)

  {
    BuyItemPackRequest request = BsonSerializer.Deserialize<BuyItemPackRequest>(document);
    Console.WriteLine("[EH] Triggered OnBuyItemPack");
    if (context.Player.TutorialState == TutorialState.NotStarted)
    {
      if (request.ItemPackId != "BasicClothes")
      {
        throw new Exception("Invalid item pack id");
      }
    }

    // Works
    var values = Enumerable.Range(0, 3).Select(_ => random.Next(0, 18)).ToArray();

    context.Player.SendPacket(new BuyItemPackResponse()
      {
        ID = NetStrings.BUY_ITEM_PACK_KEY,
        Success = "PS",
        ItemPackId = request.ItemPackId,
        ItemPackRolls = values,
      });
      
    return Task.CompletedTask;
  }
}