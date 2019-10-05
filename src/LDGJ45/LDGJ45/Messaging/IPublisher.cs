namespace LDGJ45.Messaging
{
    public interface IPublisher
    {
        void Publish<T>(T message);
    }
}