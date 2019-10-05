using System;
using System.Collections.Generic;
using LDGJ45.Core.Messaging;
using LDGJ45.Core.World.Messages;

namespace LDGJ45.Core.World
{
    public sealed class GameObject : IDisposable
    {
        private readonly List<Component> _components = new List<Component>();

        private readonly IPublisher _publisher;
        private bool _disposed;

        public GameObject(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public Transform2D Transform { get; } = new Transform2D();

        public void Dispose()
        {
            if (_disposed) return;

            for (var i = 0; i < _components.Count; i++)
            {
                RemoveComponent(_components[i]);
                i--;
            }

            _disposed = true;
        }

        public void AddComponent(Component component)
        {
            _components.Add(component);

            component.GameObject = this;
            component.Attached();

            _publisher.Publish(new ComponentAddedMessage(component));
        }

        public void RemoveComponent(Component component)
        {
            _components.Remove(component);

            component.Detached();
            component.GameObject = null;

            _publisher.Publish(new ComponentRemovedMessage(component));
        }

        public T GetComponent<T>()
            where T : Component
        {
            for (var i = 0; i < _components.Count; i++)
                if (_components[i] is T tComponent)
                    return tComponent;

            return null;
        }
    }
}