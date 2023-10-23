using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Infrastructure.Ecm.Idms;
using Pertamina.Website_KPI.Infrastructure.Ecm.None;
using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.Infrastructure.Ecm;
public static class DependencyInjection
{
    public static IServiceCollection AddEcmService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        var ecmOptions = configuration.GetSection(EcmOptions.SectionKey).Get<EcmOptions>();

        switch (ecmOptions.Provider)
        {
            case EcmProvider.None:
                services.AddNoneEcmService();
                break;
            case EcmProvider.Idms:
                services.AddIdmsEcmService(configuration, healthChecksBuilder);
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Ecm).ToUpper()} {nameof(EcmOptions.Provider)}: {ecmOptions.Provider}");
        }

        return services;
    }
}
