using IdentityProvider.Dal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SharedKernel.Extensions;

namespace IdentityProvider.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<IdentityProviderDbContext, Program>()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
