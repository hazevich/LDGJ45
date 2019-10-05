using LDGJ45.Core.World;
using Microsoft.Xna.Framework.Graphics;

namespace LDGJ45.Core.Graphics
{
    public abstract class RenderableComponent : Component, IRenderable
    {
        public int Order { get; set; } = 0;
        public abstract void Render(SpriteBatch spriteBatch);
    }
}