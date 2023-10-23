using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.CurrentUser;

namespace Pertamina.Website_KPI.Infrastructure.CurrentUser;
public static class DependencyInjection
{
    public static IServiceCollection AddCurrentUserService(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
