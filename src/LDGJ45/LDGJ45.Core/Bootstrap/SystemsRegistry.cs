using LDGJ45.Core.GameSystems;
using LDGJ45.Core.Physics;
using LDGJ45.Core.World;
using StructureMap;

namespace LDGJ45.Core.Bootstrap
{
    public sealed class SystemsRegistry : Registry
    {
        public SystemsRegistry()
        {
            ForConcreteType<GameClock>().Configure.Singleton();
            ForConcreteType<WorldSystem>().Configure.Singleton();
            ForConcreteType<UpdateableComponentsSystem>().Configure.Singleton();
            ForConcreteType<TileMapPhysicsSystem>().Configure.Singleton();

            Forward<GameClock, IGameSystem>();
            Forward<TileMapPhysicsSystem, IGameSystem>();
            Forward<UpdateableComponentsSystem, IGameSystem>();
        }
    }
}