using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Constants;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.WEARABLE_OR_WEAPON_CHANGE_KEY)]
public class OnPlayerWearableOrWeaponChange : IPacketHandler
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
        WearableOrWeaponChangeRequest request =
            BsonSerializer.Deserialize<WearableOrWeaponChangeRequest>(document);

        var world = context.World;
        if (world is null)
        {
            throw new Exception("Not in world");
        }

        if (!context.Player.HasItem(request.HotspotBlockType, InventoryItemType.WearableItem))
        {
            throw new Exception("No item in inventory");
        }

        if (!ConfigData.BlockHotSpots.TryGetValue(request.HotspotBlockType, out var list))
        {
            // Maybe send a message related to this?
            return Task.CompletedTask;
        }

        foreach (var spot in list)
        {
            context.Player.Spots[(int)spot] = request.HotspotBlockType;
        }

        if (world.Players.Count > 1)
        {
            var response = new WearableOrWeaponChangeResponse()
            {
                ID = NetStrings.WEARABLE_OR_WEAPON_CHANGE_KEY,
                PlayerId = context.Player.Id,
                HotspotBlockType = request.HotspotBlockType,
            };

            foreach (var (id, player) in world.Players)
            {
                if (id != context.Player.Id)
                {
                    player.SendPacket(response);
                }
            }
        }

        return Task.CompletedTask;
    }
}
