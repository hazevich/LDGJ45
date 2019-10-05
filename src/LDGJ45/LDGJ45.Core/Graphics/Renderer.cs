using System.Collections.Generic;
using LDGJ45.Core.Messaging;
using LDGJ45.Core.World;
using LDGJ45.Core.World.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDGJ45.Core.Graphics
{
    public sealed class Renderer
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly List<IRenderable> _renderableComponents = new List<IRenderable>();

        private readonly SpriteBatch _spriteBatch;

        public Renderer(ISubscriber subscriber, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;

            subscriber.Subscribe<ComponentAddedMessage>(Handle);
            subscriber.Subscribe<ComponentRemovedMessage>(Handle);
        }

        public Camera Camera { get; } = new Camera(640, 480, new Transform2D());

        public void Add(IRenderable renderable) => _renderableComponents.Add(renderable);
        public void Remove(IRenderable renderable) => _renderableComponents.Remove(renderable);

        public void Render()
        {
            _graphicsDevice.SetRenderTarget(null);
            _graphicsDevice.Clear(Color.AliceBlue);

            _spriteBatch.Begin(transformMatrix: Camera.WorldMatrix);

            _renderableComponents.Sort((r1, r2) => r1.Order.CompareTo(r2.Order));

            for (var i = 0; i < _renderableComponents.Count; i++) _renderableComponents[i].Render(_spriteBatch);

            _spriteBatch.End();
        }

        private void Handle(ComponentAddedMessage componentAddedMessage)
        {
            if (componentAddedMessage.Component is RenderableComponent renderableComponent)
                Add(renderableComponent);
        }

        private void Handle(ComponentRemovedMessage componentRemovedMessage)
        {
            if (componentRemovedMessage.Component is RenderableComponent renderableComponent)
                Remove(renderableComponent);
        }
    }
}