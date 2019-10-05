using System.Collections.Generic;
using LDGJ45.Gameplay;
using LDGJ45.Graphics;
using LDGJ45.Persistence;

namespace LDGJ45.World.Factories
{
    public sealed class HeroComponentsFactory : IGameObjectComponentsFactory
    {
        private readonly IAssetsDatabase _assetsDatabase;

        public HeroComponentsFactory(IAssetsDatabase assetsDatabase)
        {
            _assetsDatabase = assetsDatabase;
        }

        public GameObjectType GameObjectType { get; } = GameObjectType.Hero;

        public IEnumerable<Component> Create()
        {
            var heroSprite = _assetsDatabase.Load<Sprite>("Sprites/hero.sprite");

            yield return new SpriteRenderer(heroSprite);
            yield return new HeroComponent();
        }
    }
}