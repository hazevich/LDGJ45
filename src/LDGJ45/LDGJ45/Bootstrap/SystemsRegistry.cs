using LDGJ45.GameSystems;
using LDGJ45.World;
using StructureMap;

namespace LDGJ45.Bootstrap
{
    public sealed class SystemsRegistry : Registry
    {
        public SystemsRegistry()
        {
            ForConcreteType<WorldSystem>().Configure.Singleton();
            ForConcreteType<UpdateableComponentsSystem>().Configure.Singleton();

            Forward<UpdateableComponentsSystem, IGameSystem>();
        }
    }
}