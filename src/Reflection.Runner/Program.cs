using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reflection.Models;
using System.Reflection;
using System.Runtime.Loader;

namespace Reflection.Runner
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
                    var plugins = ResolvePluginTypes();
                    foreach (var plugin in plugins)
                    {
                        services.AddSingleton(plugin.Service, plugin.Implementation);
                    }

                    services.AddSingleton<IConfigurationComponentBase, ConfigurationComponentBase>();
                    services.AddSingleton<Runner>();
                });
        }

        public static List<Plugin> ResolvePluginTypes()
        {
            var pluginAssemblies = new List<Assembly>();
            var plugins = new List<Plugin>();
            pluginAssemblies.Add(AssemblyLoadContext.Default.LoadFromAssemblyPath(Directory.GetCurrentDirectory() + "\\Plugins\\Reflection.FileConfigurationProvider.dll"));
            pluginAssemblies.Add(AssemblyLoadContext.Default.LoadFromAssemblyPath(Directory.GetCurrentDirectory() + "\\Plugins\\Reflection.ConfigurationManagerConfigurationProvider.dll"));
            
            foreach (var assembly in pluginAssemblies)
            {
                var types = assembly.GetTypes()
                .Where(p => !p.IsInterface).Select(s => new Plugin
                {
                    Service = s.GetInterface($"I{s.Name}"),
                    Implementation = s
                }).Where(x => x.Service != null);

                plugins.AddRange(types);
            }

            return plugins;
        }
    }
}