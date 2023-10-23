using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Pertamina.Website_KPI.Application.Services.Sms;
using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.Infrastructure.Sms.Jatis;
public static class DependencyInjection
{
    public static IServiceCollection AddJatisSmsService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        services.Configure<JatisSmsOptions>(configuration.GetSection(JatisSmsOptions.SectionKey));
        services.AddSingleton<ISmsService, JatisSmsService>();

        var jatisSmsOptions = configuration.GetSection(JatisSmsOptions.SectionKey).Get<JatisSmsOptions>();

        healthChecksBuilder.AddUrlGroup(
            new Uri(jatisSmsOptions.Url),
            name: $"{nameof(Sms).ToUpper()} {CommonDisplayTextFor.Service} ({nameof(Jatis)})",
            failureStatus: HealthStatus.Degraded);

        return services;
    }
}
