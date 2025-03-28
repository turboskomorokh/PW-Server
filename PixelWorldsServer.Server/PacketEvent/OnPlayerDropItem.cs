using MongoDB.Bson;
using PixelWorldsServer.Protocol.Utils;
using PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.PLAYER_DROP_ITEM)]
public class OnPlayerDropItem : IPacketHandler
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
      // delete from player
      // put to xy on map
      // send refresh world stuff
        throw new NotImplementedException();
    }
}