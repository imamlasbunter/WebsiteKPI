using Darnton.Blazor.DeviceInterop.Geolocation;

namespace Pertamina.Website_KPI.Bsui.Services.Geolocation.None;
public static class DependencyInjection
{
    public static IServiceCollection AddNoneGeolocationService(this IServiceCollection services)
    {
        services.AddScoped<IGeolocationService, NoneGeolocationService>();

        return services;
    }
}
