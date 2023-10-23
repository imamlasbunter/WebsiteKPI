using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pertamina.Website_KPI.Infrastructure.Logging;
using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.Infrastructure.Authentication.None;
public static class DependencyInjection
{
    public static IServiceCollection AddNoneAuthenticationService(this IServiceCollection services)
    {
        LoggingHelper
            .CreateLogger()
            .LogWarning("{ServiceName} is set to None.", $"{nameof(Authentication)} {CommonDisplayTextFor.Service}");

        return services;
    }
}
