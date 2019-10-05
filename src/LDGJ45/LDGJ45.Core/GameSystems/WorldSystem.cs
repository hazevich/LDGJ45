using LDGJ45.Core.Messaging;
using LDGJ45.Core.World;
using LDGJ45.Core.World.Data;
using LDGJ45.Core.World.Factories;

namespace LDGJ45.Core.GameSystems
{
    public sealed class WorldSystem
    {
        private readonly ComponentsFactory _componentsFactory;
        private readonly IPublisher _publisher;

        private World.World _world;

        public WorldSystem(IPublisher publisher, ComponentsFactory componentsFactory)
        {
            _publisher = publisher;
            _componentsFactory = componentsFactory;
        }

        public GameObject CreateGameObject()
        {
            var gameObject = new GameObject(_publisher);
            _world.AddGameObject(gameObject);

            return gameObject;
        }

        public GameObject CreateGameObject(GameObjectType gameObjectType)
        {
            var gameObject = new GameObject(_publisher);
            _world.AddGameObject(gameObject);

            var components = _componentsFactory.GetComponents(gameObjectType);

            foreach (var component in components)
                gameObject.AddComponent(component);

            return gameObject;
        }

        public void CreateNewWorld()
        {
            _world?.Dispose();

            _world = new World.World();
        }

        public void LoadWorld(WorldData worldData)
        {
            CreateNewWorld();

            foreach (var gameObjectData in worldData.GameObjects)
            {
                CreateGameObject(gameObjectData.GameObjectType);
            }
        }
    }
}