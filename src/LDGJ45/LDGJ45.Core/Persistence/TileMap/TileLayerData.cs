using System.Collections.Generic;

namespace LDGJ45.Core.Persistence.TileMap
{
    public sealed class TileLayerData
    {
        public int Index { get; set; }
        public List<TileData> Tiles { get; set; }
    }
}