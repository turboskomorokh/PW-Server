using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.Protocol.Constants;
using PixelWorldsServer.Protocol.Packet.Request;
using PixelWorldsServer.Protocol.Packet.Response;
using PixelWorldsServer.Protocol.Utils;
using PixelWorldsServer.Protocol.Worlds;

namespace PixelWorldsServer.Server.Event;

[PacketHandler(NetStrings.WORLD_CHAT_MESSAGE_KEY)]
public class OnPlayerWorldChatMessage : IPacketHandler
{
    private const char CommandPrefix = '!';
    private const string ServerChatNickname = "{ SERVER }";

    private string[] allAdminCommands =
    {
        "give",
        "set",
        "reset inventory",
        "beta_items",
        "ALL ITEMS",
    };

    static void SendChatMessageTo(
        EventContext context,
        string worldName,
        string nickDisplayed,
        string message
    )
    {
        string coloredNick = $"<color={"#00FFFF"}>{nickDisplayed}</color>";

        var response = new WorldChatMessageResponse()
        {
            ID = NetStrings.WORLD_CHAT_MESSAGE_KEY,
            MessageBinary = new()
            {
                Time = DateTime.UtcNow,
                Nick = coloredNick,
                UserId = context.Player.Id,
                Channel = $"#{worldName}",
                MessageChat = message,
                ChannelIndex = 0,
            },
        };

        context.Player.SendPacket(response);
    }

    void ExecuteGiveCommand(EventContext context, string[] args, WorldChatMessageRequest request)
    {
        var world = context.World!;
        string msg;

        if (!Enum.TryParse(args[1], out BlockType blockType))
        {
            msg = $"'{args[1]}' NOT FOUND in BlockType.cs!";
            SendChatMessageTo(context, world.Name, ServerChatNickname, msg);
            Console.WriteLine(
                $"{context.Player.Name}: {request.Message} (NOT FOUND in BlockType.cs!)"
            );
            return;
        }

        if (!Enum.TryParse(args[2], out InventoryItemType itemType))
        {
            msg = "DEFINE CORRECT ITEM TYPE! (You can find ITEM TYPES in InventoryItemType.cs)";
            SendChatMessageTo(context, world.Name, ServerChatNickname, msg);
            Console.WriteLine($"{context.Player.Name}: {request.Message} (UNKNOWN ITEM TYPE!)");
            return;
        }

        short tmpAmount = 1;
        if (args.Length > 3 && (!short.TryParse(args[3], out tmpAmount) || tmpAmount < 1))
        {
            msg = $"'{args[3]}' is wrong amount!";
            SendChatMessageTo(context, world.Name, ServerChatNickname, msg);
            Console.WriteLine($"{context.Player.Name}: {request.Message} (WRONG AMOUNT!)");
            return;
        }

        if (!context.Player.CanFitItem(blockType, itemType, tmpAmount))
        {
            msg = "Not enough space in your inventory!";
            SendChatMessageTo(context, world.Name, ServerChatNickname, msg);
            Console.WriteLine($"{context.Player.Name}: {request.Message} (INVENTORY FULL!)");
            return;
        }

        context.Player.AddItem(blockType, itemType, tmpAmount);

        context.Player.SendPacket(
            new CollectResponse()
            {
                ID = NetStrings.COLLECT_KEY,
                IsGem = false,
                Amount = tmpAmount,
                GemType = 0,
                BlockType = blockType,
                PositionX = context.Player.Position.X,
                PositionY = context.Player.Position.Y,
                CollectableId = (int)blockType,
                InventoryType = itemType,
            }
        );

        msg = $"Given {tmpAmount} of {args[1]} to {context.Player.Name}";
        SendChatMessageTo(context, world.Name, ServerChatNickname, msg);
        Console.WriteLine($"{context.Player.Name}: {request.Message} (ITEM ADDED!)");
    }

    void SendWorldMessage(EventContext context, WorldChatMessageRequest request)
    {
        var world = context.World!;
        var response = new WorldChatMessageResponse()
        {
            ID = NetStrings.WORLD_CHAT_MESSAGE_KEY,
            MessageBinary = new()
            {
                Time = DateTime.UtcNow,
                Nick = context.Player.Name,
                UserId = context.Player.Id,
                Channel = $"#{world.Name}",
                MessageChat = request.Message,
                ChannelIndex = 0,
            },
        };

        foreach (var (id, player) in world.Players)
        {
            if (id != context.Player.Id)
            {
                player.SendPacket(response);
            }
        }
    }

    public Task Invoke(EventContext context, BsonDocument document)
    {
        WorldChatMessageRequest request = BsonSerializer.Deserialize<WorldChatMessageRequest>(
            document
        );

        var world = context.World;
        if (world is null)
        {
            throw new Exception("Not in world");
        }

        if (request.Message.StartsWith(CommandPrefix))
        {
            string[] tableCommand = request.Message.Split(' ');
            if (tableCommand.Length < 2)
            {
                string message =
                    $"Invalid command. Use ${CommandPrefix}help for available commands.";

                SendChatMessageTo(context, world.Name, ServerChatNickname, message); //Send message to client's Chat

                Console.WriteLine($"{context.Player.Name}: {request.Message} (wrong syntax!)"); //Log in console
                return Task.CompletedTask;
            }
            ExecuteGiveCommand(context, tableCommand, request);
        }

        if (world.Players.Count >= 1)
        {
            SendWorldMessage(context, request);
        }

        return Task.CompletedTask;
    }
}
