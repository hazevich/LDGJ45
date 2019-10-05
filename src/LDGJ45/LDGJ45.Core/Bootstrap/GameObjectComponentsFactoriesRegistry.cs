using LDGJ45.Core.World.Factories;
using StructureMap;

namespace LDGJ45.Core.Bootstrap
{
    public sealed class GameObjectComponentsFactoriesRegistry : Registry
    {
        public GameObjectComponentsFactoriesRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.TheCallingAssembly();
                    scanner.AddAllTypesOf<IGameObjectComponentsFactory>();
                }
            );
        }
    }
}