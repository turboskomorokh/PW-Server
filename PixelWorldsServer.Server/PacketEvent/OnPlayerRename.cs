using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;

namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.RENAME_PLAYER_KEY)]
public class OnPlayerRename : IPacketHandler
{

  private static bool IsNameOk(string playerName)
  {
    if (playerName.Length < 2 || playerName.Length > 15)
    {
      return false;
    }

    return Regex.IsMatch(playerName.ToUpper(), "^([][A-Z_^{}][][A-Z_0-9^{}-]+)$");
  }
  public async Task Invoke(EventContext context, BsonDocument document)
  {
    if(context.Database == null)
      throw new Exception("Database is null");
    PlayerRenameRequest request = BsonSerializer.Deserialize<PlayerRenameRequest>(document);

    var response = new RenamePlayerResponse()
    {
      ID = NetStrings.RENAME_PLAYER_KEY,
    };

    if (!context.Player.Name.StartsWith("SUBJECT_"))
    {
      response.IsSuccess = false;
      response.ErrorState = 8; // Already created a name
    }
    else if (!IsNameOk(request.PlayerName))
    {
      response.IsSuccess = false;
      response.ErrorState = 6; // Invalid name
    }
    else if (await context.Database.GetPlayerByNameAsync(request.PlayerName).ConfigureAwait(false) is not null)
    {
      response.IsSuccess = false;
      response.ErrorState = 7; // Already exist
    }
    else
    {
      response.IsSuccess = true;
      context.Player.Name = request.PlayerName;
      await context.Database.UpdatePlayerNameAsync(context.Player.Id, request.PlayerName).ConfigureAwait(false);
    }

    context.Player.SendPacket(response);
  }
}