using LDGJ45.Core.World;
using Microsoft.Xna.Framework.Graphics;

namespace LDGJ45.Core.Graphics
{
    public abstract class RenderableComponent : Component
    {
        public abstract void Render(SpriteBatch spriteBatch);
    }
}