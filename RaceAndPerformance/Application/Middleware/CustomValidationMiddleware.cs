using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RaceAndPerformance.Application.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RaceAndPerformance.Application.Middleware
{
    public class CustomValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomValidationMiddleware> _logger;

        public CustomValidationMiddleware(RequestDelegate next, ILogger<CustomValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errorMessage = JsonConvert.SerializeObject(new
                {
                    message = ex.Message,
                    errors = ex.Errors
                });

                _logger.LogError(errorMessage);

                await context.Response.WriteAsync(errorMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
