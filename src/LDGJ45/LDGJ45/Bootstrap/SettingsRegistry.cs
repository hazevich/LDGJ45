using Microsoft.Extensions.Configuration;
using StructureMap;

namespace LDGJ45.Bootstrap
{
    public sealed class SettingsRegistry : Registry
    {
        public SettingsRegistry()
        {
            For<Settings>().Use(LoadConfiguration());
        }

        private Settings LoadConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .AddUserSecrets<Settings>(true)
                                .Build();

            var settings = new Settings();
            configuration.Bind(settings);

            return settings;
        }
    }
}