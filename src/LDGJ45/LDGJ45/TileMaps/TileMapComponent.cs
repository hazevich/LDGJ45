using LDGJ45.World;

namespace LDGJ45.TileMaps
{
    public sealed class TileMapComponent : Component
    {
        public TileMapComponent(TileMap tileMap)
        {
            TileMap = tileMap;
        }

        public TileMap TileMap { get; }

        public void Resize(int width, int height, int tileWidth, int tileHeight)
        {
            TileMap.Resize(height, width);
            TileMap.ResizeTiles(tileWidth, tileHeight);
        }
    }
}