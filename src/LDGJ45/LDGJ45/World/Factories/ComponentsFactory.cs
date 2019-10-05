using System.Collections.Generic;
using System.Linq;

namespace LDGJ45.World.Factories
{
    public sealed class ComponentsFactory
    {
        private readonly Dictionary<GameObjectType, IGameObjectComponentsFactory> _factories;

        public ComponentsFactory(IEnumerable<IGameObjectComponentsFactory> factories)
        {
            _factories = factories.ToDictionary(f => f.GameObjectType);
        }

        public IEnumerable<Component> GetComponents(GameObjectType gameObjectType)
        {
            return _factories[gameObjectType].Create();
        }
    }
}