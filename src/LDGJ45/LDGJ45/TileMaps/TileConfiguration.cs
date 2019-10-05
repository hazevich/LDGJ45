namespace LDGJ45.TileMaps
{
    public sealed class TileConfiguration
    {
        public TileConfiguration(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        public int MapWidth { get; }
        public int MapHeight { get; }
        public int TileWidth { get; }
        public int TileHeight { get; }
    }
}