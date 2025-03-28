using MongoDB.Bson.Serialization.Attributes;
using PixelWorldsServer.Protocol.Packet;
using PixelWorldsServer.Protocol.Utils;
using PixelWorldsServer.Protocol.Worlds;

public class DropItemRequest : PacketBase
{
    [BsonElement(NetStrings.POSITION_X_KEY)]
    public int x;

    [BsonElement(NetStrings.POSITION_Y_KEY)]
    public int y;

    [BsonElement(NetStrings.PLAYER_REMOVE_DELETE_ITEM_KEY)]
    public CollectableData? data;

    [BsonElement(NetStrings.COLLECTABLE_ID_KEY)]
    public int CollectableId { get; set; }
}
