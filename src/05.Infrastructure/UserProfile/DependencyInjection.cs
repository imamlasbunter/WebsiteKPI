using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Infrastructure.UserProfile.IdAMan;
using Pertamina.Website_KPI.Infrastructure.UserProfile.IS4IM;
using Pertamina.Website_KPI.Infrastructure.UserProfile.None;
using Pertamina.Website_KPI.Shared.Common.Constants;
using Pertamina.Website_KPI.Shared.Common.Extensions;

namespace Pertamina.Website_KPI.Infrastructure.UserProfile;
public static class DependencyInjection
{
    public static IServiceCollection AddUserProfileService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        var userProfileOptions = configuration.GetSection(UserProfileOptions.SectionKey).Get<UserProfileOptions>();

        switch (userProfileOptions.Provider)
        {
            case UserProfileProvider.None:
                services.AddNoneUserProfileService();
                break;
            case UserProfileProvider.IdAMan:
                services.AddIdAManUserProfileService(configuration, healthChecksBuilder);
                break;
            case UserProfileProvider.IS4IM:
                services.AddIS4IMUserProfileService(configuration, healthChecksBuilder);
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(UserProfile).SplitWords()} {nameof(UserProfileOptions.Provider)}: {userProfileOptions.Provider}");
        }

        return services;
    }
}
