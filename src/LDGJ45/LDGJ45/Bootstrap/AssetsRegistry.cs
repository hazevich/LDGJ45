using LDGJ45.Persistence;
using StructureMap;

namespace LDGJ45.Bootstrap
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
                }
            );

            For<ISerializer>().Use<JsonSerializer>();

            ForConcreteType<FileAssetsDatabase>().Configure.Singleton();
            Forward<FileAssetsDatabase, IAssetsDatabase>();
        }
    }
}