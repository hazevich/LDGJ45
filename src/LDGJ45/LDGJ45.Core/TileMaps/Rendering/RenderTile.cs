using LDGJ45.Core.Graphics;
using Microsoft.Xna.Framework;

namespace LDGJ45.Core.TileMaps.Rendering
{
    public sealed class RenderTile
    {
        public RenderTile(Tile tile, Sprite sprite, Vector2 position)
        {
            Tile = tile;
            Sprite = sprite;
            Position = position;
        }

        public Tile Tile { get; }
        public Sprite Sprite { get; }
        public Vector2 Position { get; }
    }
}