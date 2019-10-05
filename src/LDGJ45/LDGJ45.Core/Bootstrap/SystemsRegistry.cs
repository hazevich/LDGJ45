using LDGJ45.Core.GameSystems;
using LDGJ45.Core.World;
using StructureMap;

namespace LDGJ45.Core.Bootstrap
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