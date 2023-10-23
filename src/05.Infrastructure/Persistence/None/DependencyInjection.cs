using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.Persistence;

namespace Pertamina.Website_KPI.Infrastructure.Persistence.None;
public static class DependencyInjection
{
    public static IServiceCollection AddNonePersistenceService(this IServiceCollection services)
    {
        services.AddScoped<IWebsite_KPIDbContext, NoneWebsite_KPIDbContext>();

        return services;
    }
}
