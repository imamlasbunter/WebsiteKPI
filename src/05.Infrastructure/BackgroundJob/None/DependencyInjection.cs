using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.BackgroundJob;

namespace Pertamina.Website_KPI.Infrastructure.BackgroundJob.None;
public static class DependencyInjection
{
    public static IServiceCollection AddNoneBackgroundJobService(this IServiceCollection services)
    {
        services.AddTransient<IBackgroundJobService, NoneBackgroundJobService>();

        return services;
    }
}
