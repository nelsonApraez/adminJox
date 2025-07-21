using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Package.Utilities.Net;

namespace Api.Installers
{
    public static class HealthChecks
    {
        /// <summary>
        ///     Use Health Checks dependencies.
        /// </summary>
        public static void UseHealthChecks(this IApplicationBuilder app, string endpointDescription)
        {
            app.UseHealthChecks(Constants.HealthUrl,
                new HealthCheckOptions
                {
                    ResultStatusCodes =
                    {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    },
                    ResponseWriter = (HttpContext context, HealthReport result) =>
                    {
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        return context.Response.WriteAsync(new
                        {
                            Name = endpointDescription,
                            Status = result.Status.ToString(),
                            Duration = result.TotalDuration,
                            AppName = System.Environment.MachineName,
                            Info = result.Entries.Select(e => new
                            {
                                e.Key,
                                e.Value.Description,
                                e.Value.Duration,
                                Status = e.Value.Status.ToString(),
                                Error = e.Value.Exception?.Message + ": " + e.Value.Exception?.InnerException?.Message,
                                e.Value.Data
                            }).ToList()
                        }.ToJsonSerialize());
                    },
                    AllowCachingResponses = false
                });

        }
    }


}



