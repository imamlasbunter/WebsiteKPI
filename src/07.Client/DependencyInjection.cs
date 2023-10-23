using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Client.Services.BackEnd;
using Pertamina.Website_KPI.Client.Services.HealthCheck;
using Pertamina.Website_KPI.Client.Services.UserInfo;

namespace Pertamina.Website_KPI.Client;
public static class DependencyInjection
{
    public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthCheckService();
        services.AddBackEndService(configuration);
        services.AddUserInfoService();

        return services;
    }
}
