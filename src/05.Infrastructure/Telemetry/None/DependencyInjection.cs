using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pertamina.Website_KPI.Infrastructure.Logging;
using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.Infrastructure.Telemetry.None;
public static class DependencyInjection
{
    public static IServiceCollection AddNoneTelemetryService(this IServiceCollection services)
    {
        LoggingHelper
            .CreateLogger()
            .LogWarning("{ServiceName} is set to None.", $"{nameof(Telemetry)} {CommonDisplayTextFor.Service}");

        return services;
    }
}
