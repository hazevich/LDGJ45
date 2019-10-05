using System.Collections.Generic;
using LDGJ45.GameSystems;
using LDGJ45.Messaging;
using LDGJ45.World.Messages;

namespace LDGJ45.World
{
    public sealed class UpdateableComponentsSystem : IGameSystem
    {
        private readonly List<UpdateableComponent> _components = new List<UpdateableComponent>();
        private readonly List<UpdateableComponent> _componentsToAdd = new List<UpdateableComponent>();
        private readonly List<UpdateableComponent> _componentsToRemove = new List<UpdateableComponent>();

        public UpdateableComponentsSystem(ISubscriber subscriber)
        {
            subscriber.Subscribe<ComponentAddedMessage>(Handle);
            subscriber.Subscribe<ComponentRemovedMessage>(Handle);
        }

        public void Update()
        {
            AddComponents();
            RemoveComponents();
            UpdateComponents();

            void AddComponents()
            {
                for (var i = 0; i < _componentsToAdd.Count; i++)
                {
                    _components.Add(_componentsToAdd[i]);
                    _componentsToAdd[i].Awake();
                }

                _componentsToAdd.Clear();
            }

            void RemoveComponents()
            {
                for (var i = 0; i < _componentsToRemove.Count; i++) _components.Remove(_componentsToRemove[i]);

                _componentsToRemove.Clear();
            }

            void UpdateComponents()
            {
                for (var i = 0; i < _components.Count; i++)
                    _components[i].Update();
            }
        }

        public void Add(UpdateableComponent updateableComponent)
        {
            _componentsToAdd.Add(updateableComponent);
        }

        public void Remove(UpdateableComponent updateableComponent)
        {
            _componentsToRemove.Add(updateableComponent);
        }

        private void Handle(ComponentAddedMessage msg)
        {
            if (msg.Component is UpdateableComponent updateableComponent)
                Add(updateableComponent);
        }

        private void Handle(ComponentRemovedMessage msg)
        {
            if (msg.Component is UpdateableComponent updateableComponent)
                Remove(updateableComponent);
        }
    }
}