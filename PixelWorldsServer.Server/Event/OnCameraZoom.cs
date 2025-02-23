using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[Event(NetStrings.CAMERA_ZOOM_LEVEL_UPDATE_KEY)]
public class OnChangeCameraZoomLevel : IEvent
{
  public Task Invoke(EventContext context, BsonDocument document)
  {
    ChangeCameraZoomLevelRequest request = BsonSerializer.Deserialize<ChangeCameraZoomLevelRequest>(document);
    Console.WriteLine("[EH] Triggered OnChangeCameraZoomLevel");
    context.Player.CameraZoomLevel = request.CameraZoomLevel;
    return Task.CompletedTask;
  }
}

[Event(NetStrings.CAMERA_ZOOM_VALUE_UPDATE_KEY)]
public class OnChangeCameraZoomValue : IEvent
{
  public Task Invoke(EventContext context, BsonDocument document)
  {
    ChangeCameraZoomValueRequest request = BsonSerializer.Deserialize<ChangeCameraZoomValueRequest>(document);

    Console.WriteLine("[EH] Triggered OnChangeCameraZoomValue");
    context.Player.CameraZoomValue = request.CameraZoomValue;
    return Task.CompletedTask;
  }
}