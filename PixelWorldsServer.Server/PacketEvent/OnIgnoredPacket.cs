using MongoDB.Bson;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

// [Event(NetStrings.GET_LIVE_STREAM_INFO_KEY)]
// [Event(NetStrings.UPDATE_LOCATION_STATUS_KEY)]
// [Event(NetStrings.INSTRUCTION_EVENT_COMPLETED_KEY)]
// [Event(NetStrings.ACHIEVEMENT_KEY)]
// [Event(NetStrings.GET_SCOREBOARD_DATA_KEY)]
// [Event(NetStrings.GET_FRIEND_LIST_KEY)]
// [Event("mp")] // idk what's this // 2025-02-023 - me too
// [Event(NetStrings.READY_TO_PLAY_KEY)]
[PacketHandler(NetStrings.TUTORIAL_STATE_UPDATE_KEY)]
public class OnIgnoredPacket : IPacketHandler
{
    public Task Invoke(EventContext _, BsonDocument __)
    {
        return Task.CompletedTask;
    }
}
