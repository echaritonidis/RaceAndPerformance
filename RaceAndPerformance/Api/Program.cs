using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaceAndPerformance.Core.Data;
using System;
using System.Linq;

namespace RaceAndPerformance.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    if (hostContext.Configuration["ASPNETCORE_ENVIRONMENT"] == "Test") return;

                    // Build the service provider
                    var serviceProvider = services.BuildServiceProvider();

                    // Apply any pending migrations to the database
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                        if (context != null && context.Database != null)
                        {
                            bool migrationApplied = context.Database.GetAppliedMigrations().Any();

                            if (!migrationApplied)
                            {
                                // Apply any pending migrations to the database
                                context.Database.Migrate();

                                Console.WriteLine("Migration complete!");
                            }
                            else
                            {
                                Console.WriteLine("Migration has already been applied.");
                            }
                        }
                    }
                });
    }
}
