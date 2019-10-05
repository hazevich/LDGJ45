using System;
using System.Collections.Generic;
using System.Linq;

namespace LDGJ45.Messaging
{
    public sealed class Messenger : IPublisher, ISubscriber
    {
        private readonly Dictionary<Type, List<IHandler>> _typeHandlers = new Dictionary<Type, List<IHandler>>();

        public void Publish<T>(T message)
        {
            var messageType = typeof(T);

            if (_typeHandlers.TryGetValue(messageType, out var handlers))
                for (var i = 0; i < handlers.Count; i++)
                    handlers[i].Handle(message);
        }

        public void Subscribe<T>(Action<T> handleAction)
        {
            var messageType = typeof(T);

            if (!_typeHandlers.TryGetValue(messageType, out var handlers))
            {
                handlers = new List<IHandler>();
                _typeHandlers[messageType] = handlers;
            }

            if (handlers.Any(h => h.IsSameAs(handleAction)))
                throw new InvalidOperationException(
                    $"Given handleAction is already subscribed to messages of type {messageType}"
                );

            handlers.Add(new Handler<T>(handleAction));
        }

        public void Unsubscribe<T>(Action<T> handleAction)
        {
            var messageType = typeof(T);

            if (_typeHandlers.TryGetValue(messageType, out var handlers))
            {
                var handler = handlers.SingleOrDefault(h => h.IsSameAs(handleAction));

                if (handler != null)
                {
                    handlers.Remove(handler);
                    return;
                }
            }

            throw new InvalidOperationException(
                $"Given handleAction is not subscribed to messages of type {messageType}"
            );
        }
    }
}