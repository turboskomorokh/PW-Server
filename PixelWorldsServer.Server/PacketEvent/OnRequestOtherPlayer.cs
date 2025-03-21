using MongoDB.Bson;
using PixelWorldsServer.Protocol.Constants;
using PixelWorldsServer.Protocol.Packet;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.REQUEST_OTHER_PLAYER_KEY)]
public class OnRequestOtherPlayer : IPacketHandler
{
    public Task Invoke(EventContext context, BsonDocument document)
    {
        var world = context.World;
        if (world is null)
        {
            throw new Exception("Nice try");
        }

        context.Player.SendPacket(new PacketBase() { ID = NetStrings.REQUEST_OTHER_PLAYER_KEY });
        context.Player.Position = PositionConversions.ConvertPlayerMapPointToWorldPoint(
            world.StartingPoint.X,
            world.StartingPoint.Y
        );

        var response = new AddNetworkPlayerResponse()
        {
            D = 0,
            X = context.Player.Position.X,
            Y = context.Player.Position.Y,
            ID = NetStrings.ADD_NETWORK_PLAYER_KEY,
            Age = context.Player.DateCreated.Ticks,
            Level = context.Player.Level,
            Spots = context.Player.Spots,
            IsVIP = context.Player.PermissionLevel == PlayerPermission.Admin,
            Gender = context.Player.Gender,
            XPLevel = context.Player.XP,
            Country = context.Player.CountryCode,
            PlayerId = context.Player.Id,
            Familiar = context.Player.FamiliarBlockType,
            Timestamp = 0,
            SkinIndex = context.Player.Skin,
            Animation = context.Player.Animation,
            Direction = context.Player.Direction,
            GemsAmount = context.Player.Gems,
            StatusIcon = context.Player.StatusIcon,
            FamiliarName = context.Player.FamiliarName,
            VIPEndTimeAge = context.Player.VIPEndTime.Ticks,
            FaceAnimIndex = context.Player.DefaultFaceAnimationIndex,
            PlayerUsername = context.Player.Name,
            CameFromPortal = true,
            IsFamiliarMaxLvl = context.Player.IsFamiliarMaxLvl,
        };

        foreach (var (id, player) in world.Players)
        {
            if (id != context.Player.Id)
            {
                context.Player.SendPacket(
                    new AddNetworkPlayerResponse()
                    {
                        D = 0,
                        X = player.Position.X,
                        Y = player.Position.Y,
                        ID = NetStrings.ADD_NETWORK_PLAYER_KEY,
                        Age = player.DateCreated.Ticks,
                        Level = player.Level,
                        Spots = player.Spots,
                        IsVIP = player.PermissionLevel == PlayerPermission.Admin,
                        Gender = player.Gender,
                        XPLevel = player.XP,
                        Country = player.CountryCode,
                        PlayerId = player.Id,
                        Familiar = player.FamiliarBlockType,
                        Timestamp = 0,
                        SkinIndex = player.Skin,
                        Animation = player.Animation,
                        Direction = player.Direction,
                        GemsAmount = player.Gems,
                        StatusIcon = player.StatusIcon,
                        FamiliarName = player.FamiliarName,
                        VIPEndTimeAge = player.VIPEndTime.Ticks,
                        FaceAnimIndex = player.DefaultFaceAnimationIndex,
                        CameFromPortal = false,
                        PlayerUsername = player.Name,
                        IsFamiliarMaxLvl = player.IsFamiliarMaxLvl,
                    }
                );

                player.SendPacket(response);
            }
        }

        return Task.CompletedTask;
    }
}
