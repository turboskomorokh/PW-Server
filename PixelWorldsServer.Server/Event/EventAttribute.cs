namespace PixelWorldsServer.Server.Event;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public sealed class EventAttribute : Attribute
{
    public string Id { get; set; }

    public EventAttribute(string id)
    {
        Id = id;
    }

    public static explicit operator EventAttribute(Delegate v)
    {
        throw new NotImplementedException();
    }
}
