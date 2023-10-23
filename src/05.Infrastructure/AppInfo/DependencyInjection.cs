using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pertamina.Website_KPI.Infrastructure.AppInfo;
public static class DependencyInjection
{
    public static IServiceCollection AddAppInfoService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppInfoOptions>(configuration.GetSection(AppInfoOptions.SectionKey));

        return services;
    }
}
