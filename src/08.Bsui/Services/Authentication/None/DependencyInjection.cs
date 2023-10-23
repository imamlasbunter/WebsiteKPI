using Pertamina.Website_KPI.Bsui.Services.Logging;
using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.Bsui.Services.Authentication.None;
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
