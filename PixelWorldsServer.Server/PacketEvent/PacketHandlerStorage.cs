using System.Reflection;
using Microsoft.Extensions.Logging;
using PixelWorldsServer.Server.Event;

public class PacketHandlerStorage
{
    private readonly Dictionary<string, IPacketHandler> Events = new();
    private readonly ILogger m_logger;

    public PacketHandlerStorage(ILogger logger)
    {
        m_logger = logger;
        RegisterPacketHandlers();
    }

    private void RegisterPacketHandlers()
    {
        var eventTypes =
            from t in Assembly.GetExecutingAssembly().GetTypes()
            where
                t.GetCustomAttribute<PacketHandlerAttribute>() != null
                && typeof(IPacketHandler).IsAssignableFrom(t)
            select t;

        foreach (var type in eventTypes)
        {
            try
            {
                var attribute =
                    type.GetCustomAttribute<PacketHandlerAttribute>()
                    ?? throw new Exception("Attribute is null");
                var instance =
                    Activator.CreateInstance(type) as IPacketHandler
                    ?? throw new Exception("Instance could not be created or cast to IEvent");

                Events[attribute.Id] = instance;
                m_logger.LogDebug($"Registered event: {type.Name}");
            }
            catch (Exception ex)
            {
                m_logger.LogError($"Failed to register event for type {type.Name}: {ex.Message}");
            }
        }
    }

    public IPacketHandler? GetEvent(string type)
    {
        return Events.TryGetValue(type, out var executor) ? executor : null;
    }

    public ICollection<string> GetEventList()
    {
        return Events.Keys.ToList();
    }
}
