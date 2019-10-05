namespace LDGJ45.Messaging
{
    public interface IHandler
    {
        void Handle(object message);
        bool IsSameAs(object handler);
    }
}