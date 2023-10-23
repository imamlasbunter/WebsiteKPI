using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.UserProfile;

namespace Pertamina.Website_KPI.Infrastructure.UserProfile.None;
public static class DependencyInjection
{
    public static IServiceCollection AddNoneUserProfileService(this IServiceCollection services)
    {
        services.AddTransient<IUserProfileService, NoneUserProfileService>();

        return services;
    }
}
