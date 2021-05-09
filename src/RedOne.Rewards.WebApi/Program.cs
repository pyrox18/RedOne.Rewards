using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RedOne.Rewards.Application.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RedOne.Rewards.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            if (args.Length > 0 && args.First() == "--seed-data")
            {
                Console.WriteLine("Seeding data...");

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    var adminUserService = services.GetRequiredService<IAdminUserService>();
                    await adminUserService.SeedAdminUserDataAsync();

                    var consumerUserService = services.GetRequiredService<IConsumerUserService>();
                    await consumerUserService.SeedConsumerUserDataAsync();

                    var memberLevelService = services.GetRequiredService<IMemberLevelService>();
                    await memberLevelService.SeedMemberLevelDataAsync();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Data seeding completed.");
                Console.ResetColor();
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
