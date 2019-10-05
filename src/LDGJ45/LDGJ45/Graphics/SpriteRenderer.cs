using LDGJ45.Utils;
using Microsoft.Xna.Framework.Graphics;

namespace LDGJ45.Graphics
{
    public sealed class SpriteRenderer : RenderableComponent
    {
        public SpriteRenderer(Sprite sprite)
        {
            Sprite = sprite;
        }

        public Sprite Sprite { get; }

        public override void Render(SpriteBatch spriteBatch) => 
            spriteBatch.Render(Sprite, Transform);
    }
}