using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Protocol.Packet.Request;

public class PlayerRenameRequest : PacketBase
{
    [BsonElement(NetStrings.PLAYER_USERNAME_KEY)]
    public string PlayerName { get; set; } = string.Empty;
}
