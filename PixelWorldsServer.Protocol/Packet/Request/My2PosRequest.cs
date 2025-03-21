using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PixelWorldsServer.Protocol.Constants;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Protocol.Packet.Request;

public class My2PosRequest : PacketBase
{
    [BsonElement(NetStrings.PLAYER_GRID_LOCATION_KEY)]
    public BsonBinaryData? BinData { get; set; }
}
