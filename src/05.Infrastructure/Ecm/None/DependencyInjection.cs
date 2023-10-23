using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.Ecm;

namespace Pertamina.Website_KPI.Infrastructure.Ecm.None;
public static class DependencyInjection
{
    public static IServiceCollection AddNoneEcmService(this IServiceCollection services)
    {
        services.AddTransient<IEcmService, NoneEcmService>();

        return services;
    }
}
