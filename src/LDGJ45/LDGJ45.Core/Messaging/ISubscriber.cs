using System;

namespace LDGJ45.Core.Messaging
{
    public interface ISubscriber
    {
        void Subscribe<T>(Action<T> handleAction);
        void Unsubscribe<T>(Action<T> handleAction);
    }
}