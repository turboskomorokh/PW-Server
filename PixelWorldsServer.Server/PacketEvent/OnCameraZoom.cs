using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.CAMERA_ZOOM_LEVEL_UPDATE_KEY)]
public class OnChangeCameraZoomLevel : IPacketHandler
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
        PlayerChangeCameraZoomLevelRequest request =
            BsonSerializer.Deserialize<PlayerChangeCameraZoomLevelRequest>(document);
        context.Player.CameraZoomLevel = request.CameraZoomLevel;
        return Task.CompletedTask;
    }
}

[PacketHandler(NetStrings.CAMERA_ZOOM_VALUE_UPDATE_KEY)]
public class OnChangeCameraZoomValue : IPacketHandler
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
        PlayerChangeCameraZoomValueRequest request =
            BsonSerializer.Deserialize<PlayerChangeCameraZoomValueRequest>(document);

        context.Player.CameraZoomValue = request.CameraZoomValue;
        return Task.CompletedTask;
    }
}
