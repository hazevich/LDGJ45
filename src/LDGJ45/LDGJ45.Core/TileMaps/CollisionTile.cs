using System.Runtime.Serialization;
using LDGJ45.Core.Physics;

namespace LDGJ45.Core.TileMaps
{
    [DataContract]
    public sealed class CollisionTile : ITile
    {
        public CollisionTile(int row, int column, RectangleF bounds)
        {
            Row = row;
            Column = column;
            Bounds = bounds;
        }

        [DataMember] public RectangleF Bounds { get; }

        [DataMember] public int Row { get; }

        [DataMember] public int Column { get; }
    }
}