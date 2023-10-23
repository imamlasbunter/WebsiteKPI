using Pertamina.Website_KPI.Bsui.Services.UI.MudBlazorUI;

namespace Pertamina.Website_KPI.Bsui.Services.UI;
public static class DependencyInjection
{
    public static IServiceCollection AddUIService(this IServiceCollection services)
    {
        services.AddMudBlazorUIService();

        return services;
    }
}
