﻿using MongoDB.Bson.Serialization.Attributes;
using PixelWorldsServer.Protocol.Constants;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Protocol.Packet.Response;

public class MyPosResponse : PacketBase
{
    [BsonElement(NetStrings.POSITION_X_KEY)]
    public float X { get; set; }

    [BsonElement(NetStrings.POSITION_Y_KEY)]
    public float Y { get; set; }

    [BsonElement(NetStrings.ANIMATION_KEY)]
    public AnimationNames Animation { get; set; }

    [BsonElement(NetStrings.DIRECTION_KEY)]
    public Direction Direction { get; set; }

    [BsonElement(NetStrings.TELEPORT_KEY)]
    public bool Teleport { get; set; }

    [BsonElement(NetStrings.PLAYER_ID_KEY)]
    public string PlayerId { get; set; } = string.Empty;
}
