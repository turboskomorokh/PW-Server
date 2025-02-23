using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PixelWorldsServer.DataAccess;
using PixelWorldsServer.Server.Players;
using PixelWorldsServer.Server.Worlds;
using System.Collections.Concurrent;
using System.Reflection;

namespace PixelWorldsServer.Server.Event;

public class EventContext
{
    public World? World { get; set; }
    public Database? Database;
    public WorldManager? WorldManager;
    public Player Player { get; set; } = null!;
}

public class IncomingPacket
{
    public Player Player { get; set; } = null!;
    public BsonDocument Document { get; set; } = null!;
    public AutoResetEvent AutoResetEvent { get; set; } = null!;
}

public class EventManager
{
    private readonly ILogger m_Logger;
    private EventStorage m_EventStorage;
    private Database m_Database;
    private WorldManager m_WorldManager;
    private readonly ConcurrentQueue<IncomingPacket> m_PacketQueue = new();

    public EventManager(ILogger<EventManager> logger, Database database, WorldManager worldManager)
    {
        m_Database = database;
        m_WorldManager = worldManager;
        m_Logger = logger;
        m_EventStorage = new EventStorage(m_Logger);
    }

    public void QueuePacket(BsonDocument document, Player player, AutoResetEvent autoResetEvent)
    {
        m_PacketQueue.Enqueue(new IncomingPacket()
        {
            Player = player,
            Document = document,
            AutoResetEvent = autoResetEvent
        });
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Task.Yield();

        m_Logger.LogInformation("Event manager is running!");
        while (!cancellationToken.IsCancellationRequested)
        {
            if (!m_PacketQueue.TryDequeue(out var incomingPacket))
            {
                Thread.Sleep(1);
                continue;
            }

            try
            {
                var realTask = InvokeAsync(incomingPacket.Document, incomingPacket.Player);
                var delayTask = Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);

                var task = await Task.WhenAny(realTask, delayTask);
                if (task == delayTask)
                {
                    m_Logger.LogCritical("Event takes more than 10 seconds!!!");
                }
            }
            catch (Exception exception)
            {
                m_Logger.LogError("Exception: {}", exception);
            }
            finally
            {
                incomingPacket.AutoResetEvent.Set();
            }
        }
    }

    private async Task InvokeAsync(BsonDocument document, Player player)
    {
        string id = document["ID"].AsString;

        var eventHandler = m_EventStorage.GetEvent(id);
        if (eventHandler == null)
        {
            m_Logger.LogWarning($"Unhandled packet {document.ToString()}");
            return;
        }

        var context = new EventContext()
        {
            Database = m_Database,
            WorldManager = m_WorldManager,
            World = player.World,
            Player = player
        };

        try
        {
            var task = eventHandler.Invoke(context, document);
            await task.ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            m_Logger.LogError($"Exception: {exception}");
        }
    }
}
