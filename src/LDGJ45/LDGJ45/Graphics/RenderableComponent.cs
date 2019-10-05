using LDGJ45.World;
using Microsoft.Xna.Framework.Graphics;

namespace LDGJ45.Graphics
{
    public abstract class RenderableComponent : Component
    {
        public abstract void Render(SpriteBatch spriteBatch);
    }
}