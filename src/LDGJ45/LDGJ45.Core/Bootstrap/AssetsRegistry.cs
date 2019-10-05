using LDGJ45.Core.Persistence;
using StructureMap;

namespace LDGJ45.Core.Bootstrap
{
    public sealed class AssetsRegistry : Registry
    {
        public AssetsRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.TheCallingAssembly();

                    scanner.AddAllTypesOf<IAssetReader>();
                    scanner.AddAllTypesOf<IAssetWriter>();
                }
            );

            For<ISerializer>().Use<JsonSerializer>();

            ForConcreteType<FileAssetsDatabase>().Configure.Singleton();
            Forward<FileAssetsDatabase, IAssetsDatabase>();
        }
    }
}