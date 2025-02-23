using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Constants;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[Event(NetStrings.TRY_TO_JOIN_WORLD_KEY)]
public class OnPlayerTryToJoinWorld : IEvent
{
  // TODO: fix duplicates
  private static readonly string[] m_TutorialWorldLists = new string[11]
    {
        "TUTORIAL1",
        "TUTORIAL2",
        "TUTORIAL3",
        "TUTORIAL4",
        "TUTORIAL5",
        "TUTORIAL6",
        "TUTORIAL7",
        "TUTORIAL8",
        "TUTORIAL9",
        "TUTORIAL10",
        "TUTORIAL11"
    };
  public async Task Invoke(EventContext context, BsonDocument document)
  {
    if (context.Database == null)
      throw new Exception("Database is null");
    if(context.WorldManager == null) 
      throw new Exception("WorldManager is null");

    TryToJoinWorldRequest request = BsonSerializer.Deserialize<TryToJoinWorldRequest>(document);
    
    Console.WriteLine("[EH] Triggered OnTryToJoinWorld");
    request.World = request.World.ToUpper();

    var response = new TryToJoinWorldResponse()
    {
      ID = NetStrings.TRY_TO_JOIN_WORLD_KEY,
      Biome = request.Biome,
      WorldName = request.World,
      JoinResult = WorldJoinResult.Ok,
    };

    if (request.Biome != BasicWorldBiome.Forest)
    {
      response.JoinResult = WorldJoinResult.WorldUnavailable;
    }
    else if (await context.Database.GetPlayerByNameAsync(request.World).ConfigureAwait(false) is not null)
    {
      response.JoinResult = WorldJoinResult.AlreadyHere;
    }
    else if (context.Player.TutorialState != TutorialState.NotStarted && m_TutorialWorldLists.Contains(request.World))
    {
      response.JoinResult = WorldJoinResult.NotAllowed;
    }
    else
    {
      await context.WorldManager.GetWorldAsync(request.World).ConfigureAwait(false); // Load / generate
    }

    context.Player.SendPacket(response);
  }
}