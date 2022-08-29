using Microsoft.Extensions.Configuration;

namespace VirtualFreezer.Shared.Testing
{
    public static class OptionsHelper
    {
        private const string AppSettings = "appsettings.test.json";
        
        public static TOptions GetOptions<TOptions>(string sectionName) where TOptions : class, new()
        {
            var options = new TOptions();
            var configuration = GetConfigurationRoot();
            var section = configuration.GetSection(sectionName);
            section.Bind(options);

            return options;
        }
        public static T GetValue<T>(string sectionName)
        {
            var configuration = GetConfigurationRoot();
            var value = configuration.GetValue<T>(sectionName);
            return value;
        }
        public static IConfigurationRoot GetConfigurationRoot()
            => new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile(AppSettings)
                .AddEnvironmentVariables()
                .Build();
    }
}