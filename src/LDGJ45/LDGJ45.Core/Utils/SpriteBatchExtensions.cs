using LDGJ45.Core.Graphics;
using LDGJ45.Core.Physics;
using LDGJ45.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace LDGJ45.Core.Utils
{
    public static class SpriteBatchExtensions
    {
        private static Texture2D _pixel;
        
        public static void Render(this SpriteBatch spriteBatch, Sprite sprite, Transform2D transform) =>
            spriteBatch.Draw(
                texture: sprite.TextureRegion.Texture,
                position: transform.Position,
                sourceRectangle: sprite.TextureRegion.Region,
                color: sprite.Color,
                rotation: transform.Rotation,
                origin: sprite.Origin,
                scale: transform.Scale,
                effects: transform.FlipX ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                layerDepth: 0
            );

        public static void Render(this SpriteBatch spriteBatch, Sprite sprite, Vector2 position) =>
            spriteBatch.Draw(
                texture: sprite.TextureRegion.Texture,
                position: position,
                sourceRectangle: sprite.TextureRegion.Region,
                color: sprite.Color,
                rotation: 0,
                origin: sprite.Origin,
                scale: Vector2.One, 
                effects: SpriteEffects.None,
                layerDepth: 0
            );

        public static void RenderFilledRectangle(this SpriteBatch spriteBatch, RectangleF rectangle, Color color) =>
            spriteBatch.Draw(
                texture: spriteBatch.GraphicsDevice.GetPixel(),
                position: new Vector2(rectangle.X, rectangle.Y),
                sourceRectangle: null,
                color: color,
                rotation: 0,
                origin: new Vector2(rectangle.Width / 2, rectangle.Height / 2),
                scale: new Vector2(rectangle.Width, rectangle.Height),
                effects: SpriteEffects.None,
                layerDepth: 0
            );

        private static Texture2D GetPixel(this GraphicsDevice graphicsDevice)
        {
            if (_pixel == null)
            {
                _pixel = new Texture2D(graphicsDevice, 1, 1);
                _pixel.SetData(new[] { Color.White });
            }

            return _pixel;
        }
    }
}