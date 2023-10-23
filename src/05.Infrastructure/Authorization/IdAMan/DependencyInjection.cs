using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Pertamina.Website_KPI.Application.Services.Authorization;
using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.Infrastructure.Authorization.IdAMan;
public static class DependencyInjection
{
    public static IServiceCollection AddIdAManAuthorizationService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        services.Configure<IdAManAuthorizationOptions>(configuration.GetSection(IdAManAuthorizationOptions.SectionKey));
        services.AddTransient<IAuthorizationService, IdAManAuthorizationService>();

        var idAManAuthorizationOptions = configuration.GetSection(IdAManAuthorizationOptions.SectionKey).Get<IdAManAuthorizationOptions>();

        healthChecksBuilder.Add(new HealthCheckRegistration(
            name: $"{nameof(Authorization)} {CommonDisplayTextFor.Service} ({nameof(IdAMan)})",
            instance: new IdAManAuthorizationHealthCheck(idAManAuthorizationOptions.HealthCheckUrl),
            failureStatus: HealthStatus.Unhealthy,
            tags: default));

        return services;
    }
}
