namespace LDGJ45.Core.Persistence
{
    public interface ISerializer
    {
        byte[] Serialize(object value);
        T Deserialize<T>(byte[] value);
    }
}