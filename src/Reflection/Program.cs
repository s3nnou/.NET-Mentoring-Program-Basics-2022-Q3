using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Reflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
                .AddEnvironmentVariables()
                .Build();

            var host = CreateHostBuilder(args).Build();
            host.Services.GetRequiredService<Runner>().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddTransient<IFileConfigurationProvider, FileConfigurationProvider>();
                    services.AddTransient<IConfigurationManagerConfigurationProvider, ConfigurationManagerConfigurationProvider>();
                    services.AddSingleton<IConfigurationComponentBase, ConfigurationComponentBase>();
                    services.AddSingleton<Runner>();
                });
        }
    }
}