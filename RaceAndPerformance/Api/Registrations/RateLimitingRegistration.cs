using Microsoft.Extensions.DependencyInjection;
using AspNetCoreRateLimit;

namespace RaceAndPerformance.Api.Registrations
{
    public static class RateLimitingRegistration
    {
        public static void RegisterRateLimiting(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }
    }
}