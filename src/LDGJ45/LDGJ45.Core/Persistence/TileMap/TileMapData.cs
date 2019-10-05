using System.Collections.Generic;
using LDGJ45.Core.World.Data;

namespace LDGJ45.Core.Persistence.TileMap
{
    public sealed class TileMapData : GameObjectData
    {
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }

        public IReadOnlyCollection<TileLayerData> TileLayers { get; set; }
        public CollisionLayerData CollisionLayer { get; set; }
    }
}