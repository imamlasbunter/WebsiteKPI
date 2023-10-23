using Pertamina.Website_KPI.Shared.Services.Authorization.Constants;

namespace Pertamina.Website_KPI.Bsui.Services.Authorization.IdAMan;
public static class DependencyInjection
{
    public static IServiceCollection AddIdAManAuthorizationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IdAManAuthorizationOptions>(configuration.GetSection(IdAManAuthorizationOptions.SectionKey));
        services.AddTransient<IAuthorizationService, IdAManAuthorizationService>();

        services.AddAuthorization(config =>
        {
            foreach (var permission in Permissions.All)
            {
                config.AddPolicy(permission, policy => policy.RequireClaim(AuthorizationClaimTypes.Permission, permission));
            }
        });

        return services;
    }
}
