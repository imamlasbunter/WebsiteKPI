using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.DateAndTime;

namespace Pertamina.Website_KPI.Infrastructure.DateAndTime;
public static class DependencyInjection
{
    public static IServiceCollection AddDateAndTimeService(this IServiceCollection services)
    {
        services.AddTransient<IDateAndTimeService, DateAndTimeService>();

        return services;
    }
}
