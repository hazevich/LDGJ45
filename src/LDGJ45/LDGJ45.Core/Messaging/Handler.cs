using System;

namespace LDGJ45.Core.Messaging
{
    public sealed class Handler<T> : IHandler
    {
        private readonly Action<T> _handleAction;

        public Handler(Action<T> handleAction)
        {
            _handleAction = handleAction;
        }

        public void Handle(object message)
        {
            if (message is T tMessage)
            {
                _handleAction(tMessage);
                return;
            }

            throw new InvalidOperationException(
                $"Expected message to be of type {typeof(T)}, but found {message.GetType()}."
            );
        }

        public bool IsSameAs(object handler)
        {
            return handler is Action<T> otherHandleAction && ReferenceEquals(_handleAction, otherHandleAction);
        }
    }
}