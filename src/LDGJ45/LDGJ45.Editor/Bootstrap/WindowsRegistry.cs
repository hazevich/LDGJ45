using LDGJ45.Core.TileMaps;
using LDGJ45.Editor.UI;
using StructureMap;

namespace LDGJ45.Editor.Bootstrap
{
    public sealed class WindowsRegistry : Registry
    {
        public WindowsRegistry()
        {
            ForConcreteType<TilePaletteWindow>().Configure.Singleton();

            Forward<TilePaletteWindow, IWindow>();
        }
    }
}