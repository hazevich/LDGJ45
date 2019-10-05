namespace LDGJ45.Persistence
{
    public interface ISerializer
    {
        byte[] Serialize(object value);
        T Deserialize<T>(byte[] value);
    }
}