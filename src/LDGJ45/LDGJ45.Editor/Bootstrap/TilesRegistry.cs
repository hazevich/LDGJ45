using LDGJ45.Core;
using Microsoft.Extensions.Configuration;
using StructureMap;

namespace LDGJ45.Editor.Bootstrap
{
    public sealed class TilesRegistry : Registry
    {
        public TilesRegistry()
        {
            For<TileSettings>().Use(LoadConfiguration());
        }

        private static TileSettings LoadConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .AddUserSecrets<TileSettings>(true)
                                .Build();

            var settings = new TileSettings();
            configuration.Bind(settings);

            return settings;
        }
    }
}