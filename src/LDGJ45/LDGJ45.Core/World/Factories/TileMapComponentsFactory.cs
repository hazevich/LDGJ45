using System.Collections.Generic;
using LDGJ45.Core.TileMaps;
using LDGJ45.Core.TileMaps.Rendering;

namespace LDGJ45.Core.World.Factories
{
    public class TileMapComponentsFactory : IGameObjectComponentsFactory
    {
        public GameObjectType GameObjectType { get; } = GameObjectType.TileMap;

        public IEnumerable<Component> Create()
        {
            var tileMap = new TileMap(new TileConfiguration(1000, 1000, 32, 32));

            yield return new TileMapComponent(tileMap);

            yield return new TileMapRenderer(new RenderTileMap(tileMap));
        }
    }
}