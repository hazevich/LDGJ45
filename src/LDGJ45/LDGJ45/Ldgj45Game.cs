using System.Linq;
using LDGJ45.Bootstrap;
using LDGJ45.GameSystems;
using LDGJ45.Graphics;
using LDGJ45.World.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StructureMap;

namespace LDGJ45
{
    public sealed class Ldgj45Game : Game
    {
        private readonly GraphicsDeviceManager _graphics;

        private Container _container;

        private IGameSystem[] _gameSystems;
        private Renderer _renderer;

        public Ldgj45Game()
        {
            _graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            _container = ConfigureContainer();

            _gameSystems = _container.GetAllInstances<IGameSystem>().ToArray();
            _renderer = _container.GetInstance<Renderer>();

            var worldSystem = _container.GetInstance<WorldSystem>();
            worldSystem.LoadWorld(
                new WorldData
                {
                    GameObjects = new[]
                    {
                        new GameObjectData()
                    }
                }
            );
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                var worldSystem = _container.GetInstance<WorldSystem>();
                worldSystem.LoadWorld(
                    new WorldData
                    {
                        GameObjects = new[]
                        {
                            new GameObjectData()
                        }
                    }
                );
            }

            for (var i = 0; i < _gameSystems.Length; i++)
                _gameSystems[i].Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            _renderer.Render();
        }

        private Container ConfigureContainer()
        {
            var registry = new Registry();
            registry.IncludeRegistry(new GraphicsRegistry(GraphicsDevice));
            registry.IncludeRegistry<SystemsRegistry>();
            registry.IncludeRegistry<GameObjectComponentsFactoriesRegistry>();
            registry.IncludeRegistry<MessengerRegistry>();
            registry.IncludeRegistry<SettingsRegistry>();
            registry.IncludeRegistry<AssetsRegistry>();

            return new Container(registry);
        }
    }
}