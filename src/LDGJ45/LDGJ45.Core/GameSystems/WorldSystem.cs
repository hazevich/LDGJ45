using System.Linq;
using LDGJ45.Core.Messaging;
using LDGJ45.Core.Persistence.TileMap;
using LDGJ45.Core.TileMaps;
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

        public GameObject CreateGameObject(GameObjectType gameObjectType)
        {
            var gameObject = new GameObject(_publisher, gameObjectType);
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

        public WorldData GetWorldData()
        {
            if (_world == null) return null;

            var worldData = new WorldData();
            worldData.GameObjects = new GameObjectData[_world.GameObjects.Count];

            for (var i = 0; i < _world.GameObjects.Count; i++)
            {
                var gameObject = _world.GameObjects[i];
                switch (gameObject.GameObjectType)
                {
                    case GameObjectType.Hero:
                        worldData.GameObjects[i] = new GameObjectData
                        {
                            GameObjectType = gameObject.GameObjectType,
                            Position = gameObject.Transform.Position,
                            Scale = gameObject.Transform.Scale
                        };
                        break;
                    case GameObjectType.TileMap:
                        var tileMap = gameObject.GetComponent<TileMapComponent>().TileMap;
                        var tileConfiguration = tileMap.TileConfiguration;
                        var tileMapData = new TileMapData
                        {
                            MapWidth = tileConfiguration.MapWidth,
                            MapHeight = tileConfiguration.MapHeight,
                            TileHeight = tileConfiguration.TileHeight,
                            TileWidth = tileConfiguration.TileWidth
                        };

                        var tileLayersData = tileMap.TileLayers.Select(
                                                        tl => new TileLayerData
                                                        {
                                                            Index = tl.Index,
                                                            Tiles = tl.Select(
                                                                          t => new TileData
                                                                          {
                                                                              Column = t.Column,
                                                                              Row = t.Row,
                                                                              TextureAssetId = t.Texture.Name,
                                                                              Rectangle = t.Region
                                                                          }
                                                                      )
                                                                      .ToList()
                                                        }
                                                    )
                                                    .ToList();

                        var collisionLayerData = new CollisionLayerData
                        {
                            Tiles = tileMap.CollisionLayer.Select(
                                               ct => new CollisionTileData
                                               {
                                                   Column = ct.Column,
                                                   Row = ct.Row
                                               }
                                           )
                                           .ToList()
                        };

                        tileMapData.TileLayers = tileLayersData;
                        tileMapData.CollisionLayer = collisionLayerData;
                        worldData.GameObjects[i] = tileMapData;
                        break;
                }
            }

            return worldData;
        }
    }
}