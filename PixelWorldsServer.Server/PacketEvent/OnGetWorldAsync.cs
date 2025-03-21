using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol;
using PixelWorldsServer.Protocol.Constants;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;
using PixelWorldsServer.Server.Event;
using PixelWorldsServer.Server.Worlds;

[PacketHandler(NetStrings.GET_WORLD_KEY)]
public class OnGetWorldAsync : IPacketHandler {

    // TODO: fix dups
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
      if(context.WorldManager == null)
        throw new Exception("WorldManager is null");
        GetWorldRequest request = BsonSerializer.Deserialize<GetWorldRequest>(document);

        request.World = request.World.ToUpper();

        World? world;
        if (m_TutorialWorldLists.Contains(request.World))
        {
            world = context.WorldManager.CopyTutorialWorld(request.World); // Copy world so that it doesn't override with other players
        }
        else
        {
            world = await context.WorldManager.GetExistingWorldAsync(request.World).ConfigureAwait(false);
        }

        if (world is null)
        {
            throw new Exception("Nice try :D");
        }

        var worldData = new GetWorldResponse()
        {
            ID = NetStrings.GET_WORLD_KEY,
            Size = world.Size,
            ItemId = world.ItemId,
            WorldId = world.Id,
            ItemDatas = world.ItemDatas,
            BlockLayer = world.BlockLayer,
            LayoutType = world.LayoutType,
            MusicIndex = world.MusicIndex,
            InventoryId = world.InventoryId,
            WeatherType = world.WeatherType,
            GravityMode = world.GravityMode,
            PlantedSeeds = world.PlantedSeeds,
            LightingType = world.LightingType,
            StartingPoint = world.StartingPoint,
            BlockWaterLayer = world.BlockWaterLayer,
            BlockWiringLayer = world.BlockWiringLayer,
            LayerBackgroundType = world.LayerBackgroundType,
            BlockBackgroundLayer = world.BlockBackgroundLayer,
        };

        var response = new GetWorldCompressedResponse()
        {
            ID = NetStrings.GET_WORLD_COMPRESSED_KEY,
            WorldData = LZMATools.Compress(worldData.ToBson()),
        };

        if (context.Player.TutorialState == TutorialState.NotStarted && world.Name == "PIXELSTATION")
        {
            context.Player.TutorialState = TutorialState.TutorialCompleted;
        }

        if (!world.AddPlayer(context.Player))
        {
            throw new Exception("Cannot add player to the world player list!");
        }

        context.Player.SendPacket(response);
    }
    }