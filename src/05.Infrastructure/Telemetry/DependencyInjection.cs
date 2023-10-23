using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Infrastructure.Telemetry.ApplicationInsights;
using Pertamina.Website_KPI.Infrastructure.Telemetry.None;
using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.Infrastructure.Telemetry;
public static class DependencyInjection
{
    public static IServiceCollection AddTelemetryService(this IServiceCollection services, IConfiguration configuration)
    {
        var telemetryOptions = configuration.GetSection(TelemetryOptions.SectionKey).Get<TelemetryOptions>();

        switch (telemetryOptions.Provider)
        {
            case TelemetryProvider.None:
                services.AddNoneTelemetryService();
                break;
            case TelemetryProvider.ApplicationInsights:
                services.AddApplicationInsightsTelemetryService(configuration);
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Telemetry)} {nameof(TelemetryOptions.Provider)}: {telemetryOptions.Provider}");
        }

        return services;
    }
}
