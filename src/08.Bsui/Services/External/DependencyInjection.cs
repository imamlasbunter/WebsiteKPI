using Pertamina.Website_KPI.Bsui.Services.External.Location;

namespace Pertamina.Website_KPI.Bsui.Services.External;
public static class DependencyInjection
{
    public static IServiceCollection AddExternalService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLocationExternalService(configuration);

        return services;
    }
}
