namespace LDGJ45.Core.Messaging
{
    public interface IHandler
    {
        void Handle(object message);
        bool IsSameAs(object handler);
    }
}