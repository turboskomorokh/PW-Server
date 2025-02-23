using MongoDB.Bson;
using PixelWorldsServer.Protocol.Packet;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;
using PixelWorldsServer.Server.Event;

namespace PixelWorldsServer.Server.Event;

// [Event(NetStrings.GET_LIVE_STREAM_INFO_KEY)]
// [Event(NetStrings.UPDATE_LOCATION_STATUS_KEY)]
// [Event(NetStrings.INSTRUCTION_EVENT_COMPLETED_KEY)]
// [Event(NetStrings.ACHIEVEMENT_KEY)]
// [Event(NetStrings.GET_SCOREBOARD_DATA_KEY)]
// [Event(NetStrings.GET_FRIEND_LIST_KEY)]
// [Event("mp")] // idk what's this // 2025-02-023 - me too
// [Event(NetStrings.READY_TO_PLAY_KEY)]
[Event(NetStrings.TUTORIAL_STATE_UPDATE_KEY)]

public class OnIgnoredPacket : IEvent
{
  public Task Invoke(EventContext _, BsonDocument __)
  {
    Console.WriteLine("[EH] Received ignored packet");
    return Task.CompletedTask;
  }
}