namespace PixelWorldsServer.Server.Event;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class PacketHandlerAttribute : Attribute
{
    public string Id { get; set; }

    public PacketHandlerAttribute(string id)
    {
        Id = id;
    }

    public static explicit operator PacketHandlerAttribute(Delegate v)
    {
        throw new NotImplementedException();
    }
}
