using Microsoft.Xna.Framework;

namespace LDGJ45.Core.Persistence.TileMap
{
    public sealed class TileData
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public string TextureAssetId { get; set; }
        public Rectangle Rectangle { get; set; }
    }
}