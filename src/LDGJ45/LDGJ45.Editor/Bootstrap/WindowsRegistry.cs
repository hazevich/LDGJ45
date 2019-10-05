using LDGJ45.Editor.UI;
using StructureMap;

namespace LDGJ45.Editor.Bootstrap
{
    public sealed class WindowsRegistry : Registry
    {
        public WindowsRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.TheCallingAssembly();
                    scanner.AddAllTypesOf<IWindow>();
                });
        }
    }
}