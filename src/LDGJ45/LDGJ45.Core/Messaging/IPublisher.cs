namespace LDGJ45.Core.Messaging
{
    public interface IPublisher
    {
        void Publish<T>(T message);
    }
}