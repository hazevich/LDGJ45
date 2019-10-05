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

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Sprite.TextureRegion.Texture,
                Transform.Position,
                Sprite.TextureRegion.Region,
                Sprite.Color,
                Transform.Rotation,
                Sprite.Origin,
                Transform.Scale,
                Transform.FlipX ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0
            );
        }
    }
}