using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDGJ45.Core.TileMaps
{
    public class Tile : ITile
    {
        public Tile(int row, int column, Texture2D texture, Rectangle region)
        {
            Row = row;
            Column = column;
            Texture = texture;
            Region = region;
        }

        public Texture2D Texture { get; }

        public Rectangle Region { get; }

        public int Row { get; }

        public int Column { get; }
    }
}