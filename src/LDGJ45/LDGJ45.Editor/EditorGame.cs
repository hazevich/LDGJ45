using System.Collections.Generic;
using System.Linq;
using LDGJ45.Core.Bootstrap;
using LDGJ45.Core.GameSystems;
using LDGJ45.Core.Graphics;
using LDGJ45.Editor.Bootstrap;
using LDGJ45.Editor.UI;
using Microsoft.Xna.Framework;
using StructureMap;

namespace LDGJ45.Editor
{
    public sealed class EditorGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;

        private Container _container;
        private EditorApp _editorApp;
        
        private IReadOnlyList<IGameSystem> _gameSystems;
        private Renderer _renderer;

        public EditorGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _container = ConfigureContainer();

            _gameSystems = _container.GetAllInstances<IGameSystem>()
                                     .ToArray();

            _renderer = _container.GetInstance<Renderer>();

            _editorApp = _container.GetInstance<EditorApp>();
        }

        protected override void Update(GameTime gameTime)
        {
            for (var i = 0; i < _gameSystems.Count; i++)
                _gameSystems[i].Update(gameTime);

            _editorApp.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            _renderer.Render();
            _editorApp.Render();
        }

        private Container ConfigureContainer()
        {
            var registry = new Registry();
            registry.For<GameWindow>().Use(Window);
            registry.For(typeof(IReadOnlyList<>)).Use(typeof(List<>));
            
            registry.ForConcreteType<InputSystem>().Configure.Singleton();
            registry.Forward<InputSystem, IGameSystem>();
            
            registry.IncludeRegistry(new GraphicsRegistry(GraphicsDevice));
            registry.IncludeRegistry<SystemsRegistry>();
            registry.IncludeRegistry<GameObjectComponentsFactoriesRegistry>();
            registry.IncludeRegistry<MessengerRegistry>();
            registry.IncludeRegistry<SettingsRegistry>();
            registry.IncludeRegistry<AssetsRegistry>();
            registry.IncludeRegistry<WindowsRegistry>();
            registry.IncludeRegistry<TilesRegistry>();
            
            return new Container(registry);
        }
    }
}