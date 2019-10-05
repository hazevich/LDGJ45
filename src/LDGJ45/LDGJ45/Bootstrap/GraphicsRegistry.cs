using Microsoft.Xna.Framework.Graphics;
using StructureMap;

namespace LDGJ45.Bootstrap
{
    public sealed class GraphicsRegistry : Registry
    {
        public GraphicsRegistry(GraphicsDevice graphicsDevice)
        {
            For<GraphicsDevice>().Use(graphicsDevice);
            For<SpriteBatch>().Use(new SpriteBatch(graphicsDevice));
        }
    }
}