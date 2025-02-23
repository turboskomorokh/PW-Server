using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using PixelWorldsServer.Server.Event;

public class EventStorage
{
  private readonly Dictionary<string, IEvent> Events = new();
  private readonly ILogger m_logger;

  public EventStorage(ILogger logger)
  {
    m_logger = logger;
    RegisterEvents();
  }

  private void RegisterEvents()
  {
    var eventTypes = from t in Assembly.GetExecutingAssembly().GetTypes()
                     where t.GetCustomAttribute<EventAttribute>() != null &&
                           typeof(IEvent).IsAssignableFrom(t)
                     select t;

    foreach (var type in eventTypes)
    {
      try
      {
        var attribute = type.GetCustomAttribute<EventAttribute>() ?? throw new Exception("Attribute is null");
        var instance = Activator.CreateInstance(type) as IEvent ?? throw new Exception("Instance could not be created or cast to IEvent");

        Events[attribute.Id] = instance;
        m_logger.LogDebug($"Registered event: {type.Name}");
      }
      catch (Exception ex)
      {
        m_logger.LogError($"Failed to register event for type {type.Name}: {ex.Message}");
      }
    }
  }

  public IEvent? GetEvent(string type)
  {
    return Events.TryGetValue(type, out var executor) ? executor : null;
  }

  public ICollection<string> GetEventList()
  {
    return Events.Keys.ToList();
  }
}
