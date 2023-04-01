using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RaceAndPerformance.Dal.Repository.Contracts;
using RaceAndPerformance.Application.Behaviors;
using RaceAndPerformance.Application.Services.Contracts;
using RaceAndPerformance.Application.Services.Implementations;
using RaceAndPerformance.Dal.Repository.Implementations;
using RaceAndPerformance.Application.Mapper;
using AutoMapper;

namespace RaceAndPerformance.Api.Registrations
{
    public static class DependencyRegistration
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddTransient(typeof(IMssqlRepository<>), typeof(MssqlRepository<>));
            services.AddTransient<IMatchRepository, MatchRepository>();
            services.AddTransient<IMatchService, MatchService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}