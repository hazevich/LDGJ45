using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDGJ45.TileMaps
{
    [DataContract]
    public class Tile : ITile
    {
        public Tile(int row, int column, Texture2D texture, Rectangle region)
        {
            Row = row;
            Column = column;
            Texture = texture;
            Region = region;
        }

        [DataMember] public Texture2D Texture { get; }

        [DataMember] public Rectangle Region { get; }

        [DataMember] public int Row { get; }

        [DataMember] public int Column { get; }
    }
}