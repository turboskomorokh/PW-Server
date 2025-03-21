using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PixelWorldsServer.Protocol.Constants;
using PixelWorldsServer.Protocol.Players;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Protocol.Packet.Request;

public class PlayerRemoveItemRequest : PacketBase
{
    [BsonElement(NetStrings.PLAYER_REMOVE_ITEM_KEY)]
    public int CollectableId { get; set; }
    
    [BsonElement(NetStrings.BLOCK_TYPE_KEY)]
    public InventoryItemBase? BlockType { get; set; }

}

// { "ID" : "RIi", "dI" : { "CollectableID" : 0, "BlockType" : 359, "Amount" : 1, "InventoryType" : 4, "PosX" : 0.0, "PosY" : 0.0, "IsGem" : false, "GemType" : 0 } }


// { "ID" : "Di", "x" : 41, "y" : 30, "dI" : { "CollectableID" : 0, "BlockType" : 162, "Amount" : 1, "InventoryType" : 4, "PosX" : 0.0, "PosY" : 0.0, "IsGem" : false, "GemType" : 0 } }
