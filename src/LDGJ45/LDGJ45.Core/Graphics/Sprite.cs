using Microsoft.Xna.Framework;

namespace LDGJ45.Core.Graphics
{
    public sealed class Sprite
    {
        public Sprite(TextureRegion2D textureRegion)
        {
            TextureRegion = textureRegion;

            Origin = new Vector2(textureRegion.Region.Width / 2, textureRegion.Region.Height / 2);
        }

        public string Name { get; set; }
        public TextureRegion2D TextureRegion { get; set; }
        public Color Color { get; set; } = Color.White;
        public Vector2 Origin { get; set; }
    }
}