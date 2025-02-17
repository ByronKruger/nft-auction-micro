using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;

namespace Shared;

public static class Setup
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration config, string appName)
    {
        var otel = services.AddOpenTelemetry();

        otel.ConfigureResource(resource => resource
            .AddService(
                serviceName: appName,
                serviceVersion: "0.0.1"
            ))
        .WithTracing(tracing => tracing
            .AddSource(appName)
            .AddAspNetCoreInstrumnetation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter());

        return services;
    }
}
