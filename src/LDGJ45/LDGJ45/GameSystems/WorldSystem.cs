using LDGJ45.Messaging;
using LDGJ45.World;
using LDGJ45.World.Data;
using LDGJ45.World.Factories;

namespace LDGJ45.GameSystems
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
                var gameObject = CreateGameObject();
                var components = _componentsFactory.GetComponents(gameObjectData.GameObjectType);

                foreach (var component in components)
                    gameObject.AddComponent(component);
            }
        }
    }
}