using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.Storage;

namespace Pertamina.Website_KPI.Infrastructure.Storage.None;
public static class DependencyInjection
{
    public static IServiceCollection AddNoneStorageService(this IServiceCollection services)
    {
        services.AddSingleton<IStorageService, NoneStorageService>();

        return services;
    }
}
