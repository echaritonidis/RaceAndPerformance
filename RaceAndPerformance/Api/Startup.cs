using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaceAndPerformance.Api.Registrations;
using FluentValidation;
using AspNetCoreRateLimit;
using RaceAndPerformance.Application.Commands.MatchCommand;
using Microsoft.OpenApi.Models;
using RaceAndPerformance.Api.Settings;
using RaceAndPerformance.Application.Middleware;
using Microsoft.Extensions.Logging;

namespace RaceAndPerformance.Api
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
            });

            // Configure database connection string options
            services.Configure<DatabaseSettings>(options =>
            {
                options.ConnectionString = _configuration["DatabaseSettings:ConnectionString"];
            });

            // Register Database
            services.RegisterDatabase(isTest: _configuration["ASPNETCORE_ENVIRONMENT"] == "Test");

            // Configure rate limiting options
            services.Configure<IpRateLimitOptions>(_configuration.GetSection("IpRateLimiting"));

            // Register Services, Repositories
            services.RegisterDependencies();

            // Register Rate Limiting services
            services.RegisterRateLimiting();

            // Api versioning registration
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine
                (
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("x-api-version")
                );
            });

            // Register healthcheck for our mssql
            services
                .AddHealthChecks()
                .AddSqlServer(_configuration["DatabaseSettings:ConnectionString"], name: "RaceAndPerformance");

            services.AddControllers();
            
            // Register swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Race and performance Api UI",
                    Description = "Race and performance Api",
                });
            });

            // Register FluentValidation
            services.AddValidatorsFromAssembly(typeof(CreateMatchCommand).Assembly);

            // Register Mediatr
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateMatchCommand>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<CustomValidationMiddleware>();

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Race and performance Api v1");
            });

            app.UseRouting();

            app.UseApiVersioning();

            app.UseIpRateLimiting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc");
            });
        }
    }
}
