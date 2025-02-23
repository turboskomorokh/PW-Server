using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[Event(NetStrings.MENU_WORLD_LOAD_INFO_KEY)]
public class OnMenuWorldLoadInfo : IEvent {
  public async Task Invoke(EventContext context, BsonDocument document)
  {
    if(context.WorldManager == null)
      throw new Exception("WorldManager is null");
    if(context.Database == null)
      throw new Exception("Database is null");

    MenuWorldLoadInfoRequest request = BsonSerializer.Deserialize<MenuWorldLoadInfoRequest>(document);
    Console.WriteLine("[EH] Triggered OnMenuWorldLoadInfo");
    request.WorldName = request.WorldName.ToUpper();

    var worldCache = await context.WorldManager.GetExistingWorldAsync(request.WorldName).ConfigureAwait(false);
    if (worldCache is not null)
    {
      context.Player.SendPacket(new MenuWorldLoadInfoResponse()
      {
        ID = NetStrings.MENU_WORLD_LOAD_INFO_KEY,
        Count = worldCache.Players.Count,
        WorldName = request.WorldName
      });
      return;
    }

    var worldModel = await context.Database.GetWorldByNameAsync(request.WorldName).ConfigureAwait(false);
    if (worldModel is not null)
    {
      context.Player.SendPacket(new MenuWorldLoadInfoResponse()
      {
        ID = NetStrings.MENU_WORLD_LOAD_INFO_KEY,
        Count = 0,
        WorldName = request.WorldName
      });
      return;
    }

    context.Player.SendPacket(new MenuWorldLoadInfoResponse()
    {
      ID = NetStrings.MENU_WORLD_LOAD_INFO_KEY,
      Count = -3,
      WorldName = request.WorldName
    });
  }
}