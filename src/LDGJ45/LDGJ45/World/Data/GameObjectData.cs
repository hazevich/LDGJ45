using Microsoft.Xna.Framework;

namespace LDGJ45.World.Data
{
    public sealed class GameObjectData
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; } = Vector2.One;
        public GameObjectType GameObjectType { get; set; }
    }
}