using System.Runtime.Serialization.Formatters.Binary;
public class PacketWriter : BinaryWriter
{
    private MemoryStream ms;
    private BinaryFormatter bf;

    public PacketWriter() : base()
    {
        ms = new MemoryStream();
        bf = new BinaryFormatter();
        OutStream = ms;
    }

    public byte[] GetBytes()
    {
        Close();

        byte[] data = ms.ToArray();

        return data;
    }
}

public class PacketReader : BinaryReader
{
    private BinaryFormatter bf;
    public PacketReader(byte[] data) : base(new MemoryStream(data))
    {
        bf = new BinaryFormatter();
    }
}