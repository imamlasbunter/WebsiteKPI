using Pertamina.Website_KPI.Bsui.Services.Authorization.IdAMan;
using Pertamina.Website_KPI.Bsui.Services.Authorization.IS4IM;
using Pertamina.Website_KPI.Bsui.Services.Authorization.None;
using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.Bsui.Services.Authorization;
public static class DependencyInjection
{
    public static IServiceCollection AddAuthorizationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthorizationOptions>(configuration.GetSection(AuthorizationOptions.SectionKey));
        var authorizationOptions = configuration.GetSection(AuthorizationOptions.SectionKey).Get<AuthorizationOptions>();

        switch (authorizationOptions.Provider)
        {
            case AuthorizationProvider.None:
                services.AddNoneAuthorizationService();
                break;
            case AuthorizationProvider.IdAMan:
                services.AddIdAManAuthorizationService(configuration);
                break;
            case AuthorizationProvider.IS4IM:
                services.AddIS4IMAuthorizationService(configuration);
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Authorization)} {nameof(AuthorizationOptions.Provider)}: {authorizationOptions.Provider}");
        }

        return services;
    }
}
