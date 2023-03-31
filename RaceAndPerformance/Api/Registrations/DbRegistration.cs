using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RaceAndPerformance.Api.Settings;
using RaceAndPerformance.Core.Data;
using System;

namespace RaceAndPerformance.Api.Registrations
{
    public static class DbRegistration
    {
        public static void RegisterDatabase(this IServiceCollection services, bool isTest)
        {
            services.AddDbContext<DataContext>((serviceProvider, optionsBuilder) =>
            {
                // Check if we're in a testing environment
                var value = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                if (isTest)
                {
                    // Use the in-memory database
                    optionsBuilder.UseInMemoryDatabase("TestDb");
                }
                else
                {
                    // Use the MSSQL database
                    var connectionString = serviceProvider.GetService<IOptions<DatabaseSettings>>().Value.ConnectionString;
                    optionsBuilder.UseSqlServer(connectionString);
                }
            });
        }
    }
}