using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OopFundamentalsAndDesignPrinciples.Services;
using OopFundamentalsAndDesignPrinciples.Models;

namespace OopFundamentalsAndDesignPrinciples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Services.GetRequiredService<IMenu>().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddTransient<ICacheService<Document>, DocumentCacheService>();
                    services.AddTransient<IFileRepository, FileRepository>();
                    services.AddTransient<IDocumentService, DocumentService>();
                    services.AddTransient<IMenu, ConsoleMenu>();
                });
        }
    }
}