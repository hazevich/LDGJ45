using Microsoft.Xna.Framework.Graphics;

namespace LDGJ45.Core.Graphics
{
    public interface IRenderable
    {
        int Order { get; }
        void Render(SpriteBatch spriteBatch);
    }
}