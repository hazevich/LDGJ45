using System.Collections.Generic;

namespace LDGJ45.Core.Persistence.TileMap
{
    public class CollisionLayerData
    {
        public IReadOnlyCollection<CollisionTileData> Tiles { get; set; }
    }
}