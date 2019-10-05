using LDGJ45.World.Factories;
using StructureMap;

namespace LDGJ45.Bootstrap
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