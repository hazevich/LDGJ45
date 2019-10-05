using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDGJ45.Core.Graphics
{
    public sealed class TextureRegion2D
    {
        public TextureRegion2D(Texture2D texture)
            : this(texture, new Rectangle(0, 0, texture.Width, texture.Height))
        {
        }

        public TextureRegion2D(Texture2D texture, Rectangle region)
        {
            Texture = texture;
            Region = region;
        }


        public Texture2D Texture { get; }
        public Rectangle Region { get; }
    }
}